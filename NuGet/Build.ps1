. ./Package-Tools.ps1

function Get-BinaryPackagePaths {
    [CmdletBinding()]
    param ([string]$BasePath = ".")
    return @(
        @("$BasePath\WinApi.nuspec", "$BasePath\..\WinApi\"),
        @("$BasePath\WinApi.Desktop.nuspec", "$BasePath\..\WinApi.Desktop\"),
        @("$BasePath\WinApi.Utils.nuspec", "$BasePath\..\WinApi.Utils\"),
        @("$BasePath\WinApi.DxUtils.nuspec", "$BasePath\..\WinApi.DxUtils\"),
        @("$BasePath\WinApi.Windows.Controls.nuspec", "$BasePath\..\WinApi.Windows.Controls\")
    );
}

function Get-SourcePackagePaths {
    [CmdletBinding()]
    param ([string]$BasePath = ".")
    return @(
        "$BasePath\WinApi.Source.nuspec",
        "$BasePath\WinApi.Universe.Source.nuspec"
    );
}

function Build-BinaryPackagesWithVersion {
    [CmdletBinding()]
    param (
        [string]$BasePath = ".",        
        [string]$Version
    )

    Get-BinaryPackagePaths $BasePath | % {
        Run-CsPackageBuildPackWithVersion $_[0] -CsSourcePath $_[1] -Version $Version -Verbose;
    }
}

function Build-BinaryPackages {
    [CmdletBinding()]
    param (
        [string]$BasePath = ".",        
        [switch]$Major,
        [switch]$Minor,
        [switch]$Patch,
        [switch]$Build,
        [int]$Increment = 1
    )

    $vParams = @{ Major = $Major; 
        Minor = $Minor; 
        Patch = $Patch; 
        Build = $Build; 
        Increment = $Increment };

    Get-BinaryPackagePaths $BasePath | % {
        Run-CsPackageBuildPackIncrementalVersion $_[0] -CsSourcePath $_[1] -Verbose @vParams;
    }
}

function Build-SourcePackagesWithVersion {
    [CmdletBinding()]
    param (
        [string]$BasePath = ".",        
        [string]$Version
    )

    Get-SourcePackagePaths $BasePath | % {
        Run-NuspecPackWithVersion $_ -Version $Version;
    }
}

function Build-SourcePackages {
    [CmdletBinding()]
    param (
        [string]$BasePath = ".",        
        [switch]$Major,
        [switch]$Minor,
        [switch]$Patch,
        [switch]$Build,
        [int]$Increment = 1
    )

    $vParams = @{ Major = $Major; 
        Minor = $Minor; 
        Patch = $Patch; 
        Build = $Build; 
        Increment = $Increment };

    Get-SourcePackagePaths $BasePath | % {
        $path = $_;
        $version = Get-NuSpecVersionFromPath $path -Verbose:$verbose;
        $version = Get-IncrementedVersion $version @vParams;
        Run-NuspecPackWithVersion $path -Version $version;
    }
}

function Build-All {
    [CmdletBinding()]
    param (
        [string]$BasePath = ".",        
        [switch]$Major,
        [switch]$Minor,
        [switch]$Patch,
        [switch]$Build,
        [int]$Increment = 1
    )

    $vParams = @{ Major = $Major; 
        Minor = $Minor; 
        Patch = $Patch; 
        Build = $Build; 
        Increment = $Increment };

    Clean-Nugets $BasePath;
    Build-BinaryPackages -BasePath $BasePath @vParams;
    Build-SourcePackages -BasePath $BasePath @vParams;
}

function Build-AllWithVersion {
    [CmdletBinding()]
    param (
        [string]$BasePath = ".",        
        [string]$Version
    )
    Clean-Nugets $BasePath;
    Build-BinaryPackagesWithVersion -BasePath $BasePath -Version $Version;
    Build-SourcePackagesWithVersion -BasePath $BasePath -Version $Version;
}

