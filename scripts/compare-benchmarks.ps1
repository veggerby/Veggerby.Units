param(
    [Parameter(Mandatory=$true)][string]$BaselinePath,
    [Parameter(Mandatory=$true)][string]$CurrentPath,
    [double]$AllowedMeanRegressionPercent = 1.0
)

# Simple JSON structures: { testName: { MeanNs: <number>, AllocB: <number> }, ... }
$baseline = Get-Content $BaselinePath | ConvertFrom-Json
$current = Get-Content $CurrentPath | ConvertFrom-Json

$failures = @()
foreach ($name in $baseline.PSObject.Properties.Name) {
    if (-not $current.PSObject.Properties.Name.Contains($name)) {
        $failures += "Missing benchmark '$name' in current results"
        continue
    }
    $b = $baseline.$name
    $c = $current.$name
    if ($b.MeanNs -eq 0) { continue } # Skip uninitialized baseline rows
    $regression = (($c.MeanNs - $b.MeanNs) / $b.MeanNs) * 100.0
    if ($regression -gt $AllowedMeanRegressionPercent) {
        $failures += "Mean regression $regression% > $AllowedMeanRegressionPercent% for $name (baseline=$($b.MeanNs)ns current=$($c.MeanNs)ns)"
    }
    if ($c.AllocB -gt $b.AllocB) {
        $failures += "Alloc increase for $name (baseline=$($b.AllocB)B current=$($c.AllocB)B)"
    }
}

if ($failures.Count -gt 0) {
    Write-Error ("PERFORMANCE REGRESSIONS:\n" + ($failures -join "\n"))
    exit 1
}
else {
    Write-Host "No performance regressions detected."
}
