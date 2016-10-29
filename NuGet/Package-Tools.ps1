function Pack-Nugets {
    [CmdletBinding()]
    param(
        [string]$Src = ".", 
        [string]$Destination = "."
    )
    $resSrc = $(resolve-path $Src | select -ExpandProperty Path);
    $resDest = $(resolve-path $Destination | select -ExpandProperty Path);
    Write-Verbose "Starting nuget pack from *.nuspec in $resSrc";
    ls -Path $resSrc -Filter *.nuspec | %{ nuget pack $_ -OutputDirectory $resDest };
    Write-Verbose "Packaged to $resDest";
}

function Clean-Nugets {
    [CmdletBinding()]
    param(
        [string]$Src = ".",
        [Switch]$Recurse = $False
    )
    $resSrc = $(resolve-path $Src | select -ExpandProperty Path);
    if ($Recurse) {
        Write-Verbose "Cleaning **.nupkg from $resSrc";        
        del $resSrc\**.nupkg
    } else {
        Write-Verbose "Cleaning *.nupkg from $resSrc";
        del $resSrc\*.nupkg
    }
}

function Push-Nuget {
    [CmdletBinding()]
    param(
        [string]$Path,
        [string]$Source = "nuget.org",
        [string]$ApiKey = $Null
    )
    $cmd = "nuget push $Path";
    if ($Source) {
        $cmd += " -Source $Source";
    }
    if ($ApiKey) {
        $cmd += " -ApiKey $ApiKey";
    }
    Invoke-Expression $cmd;
}

function Push-Nugets {
    [CmdletBinding()]
    param(
        [string]$Src = ".",
        [string]$Source = "nuget.org",
        [string]$ApiKey = $Null
    )
    $resSrc = $(resolve-path $Src | select -ExpandProperty Path);
    Write-Verbose "Pushing nugets from $resSrc";
    $nugetArg = "";
    if ($Source) {
        $nugetArg += "-Source $Source";
    }
    if ($ApiKey) {
        $nugetArg += "-ApiKey $ApiKey";
    }
    ls -Path $resSrc -Filter "*.nupkg" | %{ Invoke-Expression "Push-Nuget -Path $_ $nugetArg" }
}

function Find-FirstFile {
    [CmdletBinding()]
    param(
        [string]$Path = ".",
        [string]$Filter = "*",        
        [switch]$NoRecurse = $False)
        $cmd = "ls -Path $Path -Filter $Filter";
        if (-not $NoRecurse) {
                $cmd += " -Recurse";
        }
        $file = Invoke-Expression $cmd | select -First 1 -ExpandProperty FullName;
        return $file;
}

function Get-CsAssemblyVersionFromPath($path) {
        Write-Verbose "Using CsAssemblyVersion from: $path";
        $path = (Resolve-Path $path | select -ExpandProperty Path);        
        $data = cat $path;
        $m = $data | sls -Pattern "^\s*\[assembly:\s*AssemblyVersion\(`"(.*)`"\)\]";
        if ($m) {
            $version = $m.Matches[0].Groups[1].Value;
            return New-Object Version($version);
        } else {
            Write-Error "Version info not found in $path";
        }
}

function Get-NuSpecVersionFromPath($path) {
        Write-Verbose "Using NuSpec from: $path";
        $path = (Resolve-Path $path | select -ExpandProperty Path);        
        $data = cat $path;
        $m = $data | sls -Pattern "^\s*<version>(.*)</version>";
        if ($m) {
            $version = $m.Matches[0].Groups[1].Value;
            return New-Object Version($version);
        } else {
            Write-Error "Version info not found in $path";
        }
}

function Set-CsAssemblyVersionForPath($path, $version) {
        Write-Verbose "Setting CsAssemblyVersion for: $path";
        Write-Verbose "Version: $version";
        $path = (Resolve-Path $path | select -ExpandProperty Path);        
        $data = [System.IO.File]::ReadAllLines($path);
        if ($data) {
            $data = $data -replace "^\s*\[assembly:\s*AssemblyVersion\((.*)\)\]", "[assembly: AssemblyVersion(`"$version`")]";
            $data = $data -replace "^\s*\[assembly:\s*AssemblyFileVersion\((.*)\)\]", "[assembly: AssemblyFileVersion(`"$version`")]";
            $data = $data -replace "^\s*\[assembly:\s*AssemblyInformationalVersion\((.*)\)\]", "[assembly: AssemblyInformationalVersion(`"$version`")]";
            [System.IO.File]::WriteAllLines($path, $data);
            Write-Verbose "CsAssemblyVersion set successfully";            
        }
}

function Set-NuSpecVersionForPath($path, $version) {
        Write-Verbose "Setting NuSpecVersion for: $path";
        Write-Verbose "Version: $version";
        $path = (Resolve-Path $path | select -ExpandProperty Path);
        $data = [System.IO.File]::ReadAllLines($path);
        if ($data) {
            $data = $data -replace "^(\s*)<version>(.*)</version>", "`$1<version>$version</version>";
            [System.IO.File]::WriteAllLines($path, $data);
            Write-Verbose "NuSpecVersion set successfully";
        }
}

function Get-IncrementedVersion {
    param(
    [Version]$version,
    [switch]$Major,
    [switch]$Minor,
    [switch]$Patch,
    [switch]$Build,
    [int]$Increment = 1)

    $vMajor = $version.Major;
    $vMinor = $version.Minor;
    $vPatch = $version.Build;
    $vBuild = $version.Revision;

    if ($Major) { $vMajor += $Increment; }
    if ($Minor) { $vMinor += $Increment; }
    if ($Patch) { $vPatch += $Increment; }
    if ($Build) { $vBuild += $Increment; }
    return new-object Version($vMajor, $vMinor, $vPatch, $vBuild);
}

function Get-CsAssemblyVersion {
    [CmdletBinding()]
    param(
        [string]$Path = ".",        
        [string]$Filter = "AssemblyInfo.cs",
        [switch]$NoRecurse = $False)

        $verbose = $PSBoundParameters['Verbose'] -eq $true;
        $params = @{ Path = $Path; Filter = $Filter; NoRecurse = $NoRecurse }
        $file = Find-FirstFile @params;
        if ($file) {
            return Get-CsAssemblyVersionFromPath $file -verbose:$verbose;
        } else {
            Write-Error "Unable to determine CsAssemblyVersion"
        }
}

function Set-CsAssemblyVersion {
    [CmdletBinding()]
    param(
        [string]$Path = ".",
        [string]$Filter = "AssemblyInfo.cs",        
        [switch]$NoRecurse = $False,
        [string]$Version)

        $verbose = $PSBoundParameters['Verbose'] -eq $true;
        $params = @{ Path = $Path; Filter = $Filter; NoRecurse = $NoRecurse }
        $file = Find-FirstFile @params;
        if ($file) {
            Set-CsAssemblyVersionForPath $file $Version -verbose:$verbose;
        } else {
            Write-Error "Unable to find CsAssemblyVersion"
        }
}

function Set-NuSpecVersion {
    [CmdletBinding()]
    param(
        [string]$Path = ".",
        [string]$Filter = "*.nuspec",
        [switch]$NoRecurse = $False,
        [string]$Version)

        $verbose = $PSBoundParameters['Verbose'] -eq $true;
        $params = @{ Path = $Path; Filter = $Filter; NoRecurse = $NoRecurse }
        $file = Find-FirstFile @params;
        if ($file) {
            Set-NuSpecVersionForPath $file $Version -verbose:$verbose;
        } else {
            Write-Error "Unable to find NuSpecVersion"
        }
}

function Set-CsPackageVersion {
    [CmdletBinding()]
    param(
        [string]$NuSpec,
        [string]$CsAssemblyInfoFile,
        [string]$Version)
        
        $verbose = $PSBoundParameters['Verbose'] -eq $true;
        Set-CsAssemblyVersionForPath $CsAssemblyInfoFile $Version -verbose:$verbose
        Set-NuSpecVersionForPath $NuSpec $Version -verbose:$verbose
}

function Run-MsBuild {
    [CmdletBinding()]
    param(
        [string]$ExecDir = ".",
        [string]$ProjectFile = $Null,
        [string]$Configuration = "Release"
    )

    Push-Location;
    cd (Resolve-Path $ExecDir | select -ExpandProperty Path);
    $cmd = "msbuild";
    if ($ProjectFile) {
        $cmd += " $ProjectFile";
    }
    if ($Configuration) {
        $cmd += " /p:Configuration=$Configuration";
    }
    Invoke-Expression $cmd;
    Pop-Location;
}

function Run-NuspecPackWithVersion {
    [CmdletBinding()]
    param(
    [string]$NuSpec,
    [string]$Version,
    [string]$Destination = ".")

    $verbose = $PSBoundParameters['Verbose'] -eq $true;
    Set-NuSpecVersionForPath $NuSpec $Version -verbose:$verbose;
    Pack-Nugets $NuSpec -Destination $Destination -Verbose:$verbose;
}

function Run-CsPackageIncrementVersion {
    [CmdletBinding()]
    param(
        [string]$NuSpec,
        [string]$CsAssemblyInfoPath = ".",
        [string]$CsAssemblyInfoFilter = "AssemblyInfo.cs",
        [switch]$NoRecurse = $False,
        [switch]$Major,
        [switch]$Minor,
        [switch]$Patch,
        [switch]$Build,
        [int]$Increment = 1)

        $verbose = $PSBoundParameters['Verbose'] -eq $true;
        $params = @{ Path = $CsAssemblyInfoPath; 
            Filter = $CsAssemblyInfoFilter; 
            NoRecurse = $NoRecurse; };
        $csAssemFile = Find-FirstFile @params;
        if (-not $csAssemFile) { return; }
        $v = Get-CsAssemblyVersionFromPath $csAssemFile -Verbose:$verbose;
        if (-not $v) { return; }
        $vParams = @{ Major = $Major; Minor = $Minor; Patch = $Patch; Build = $Build; Increment = $Increment };
        $v = Get-IncrementedVersion $v @vParams;
        Set-CsPackageVersion $NuSpec $csAssemFile $v -Verbose:$verbose;
}


function Run-CsPackageBuildPackWithVersion {
    [CmdletBinding()]
    param(
    [string]$NuSpec,
    [string]$Version,
    [string]$CsSourcePath = ".",
    [string]$CsAssemblyInfoPath = $Null,
    [string]$CsAssemblyInfoFilter = "AssemblyInfo.cs",
    [switch]$NoRecurse = $False,
    [string]$Destination = ".")

    $verbose = $PSBoundParameters['Verbose'] -eq $true;
    if (-not $CsAssemblyInfoPath) {
        $CsAssemblyInfoPath = $CsSourcePath;
    }

    $params = @{ Path = $CsAssemblyInfoPath; 
        Filter = $CsAssemblyInfoFilter; 
        NoRecurse = $NoRecurse; };
    $csAssemFile = Find-FirstFile @params;
    if (-not $csAssemFile) { return; }
    Set-CsPackageVersion $NuSpec $csAssemFile $Version -Verbose:$verbose;
    Run-MsBuild $CsSourcePath;
    Pack-Nugets $NuSpec -Destination $Destination -Verbose:$verbose;
}

function Run-CsPackageBuildPackIncrementalVersion {
    [CmdletBinding()]
    param(
        [string]$NuSpec,
        [string]$CsSourcePath = ".",
        [string]$CsAssemblyInfoPath = $Null,
        [string]$CsAssemblyInfoFilter = "AssemblyInfo.cs",
        [switch]$NoRecurse = $False,
        [switch]$Major,
        [switch]$Minor,
        [switch]$Patch,
        [switch]$Build,
        [int]$Increment = 1, 
        [string]$Destination = ".")

        $verbose = $PSBoundParameters['Verbose'] -eq $true;
        if (-not $CsAssemblyInfoPath) {
            $CsAssemblyInfoPath = $CsSourcePath;
        }
        Write-Verbose "NuSpec: $NuSpec";
        Write-Verbose "Source Path: $CsSourcePath";
        Write-Verbose "AssemblyInfo Search Path: $CsAssemblyInfoPath\$CsAssemblyInfoFilter";

        $params = @{ NuSpec = $NuSpec; 
            CsAssemblyInfoPath = $CsAssemblyInfoPath;
            CsAssemblyInfoFilter = $CsAssemblyInfoFilter;
            NoRecurse = $NoRecurse;
            Major = $Major;
            Minor = $Minor;
            Patch = $Patch;
            Build = $Build;
            Increment = $Increment;
            Verbose = $verbose;
        };
        Run-CsPackageIncrementVersion @params;
        Run-MsBuild $CsSourcePath;
        Pack-Nugets $NuSpec -Destination $Destination -Verbose:$verbose;
}