$ErrorActionPreference = "Stop"

$solutionPath = "RoundTable.slnx"
$resultsDir   = "./TestResults"
$reportDir    = "./CoverageReport"

# 1. Ensure ReportGenerator global tool is installed
if (-not (Get-Command reportgenerator -ErrorAction SilentlyContinue)) {
    Write-Host "Installing dotnet-reportgenerator-globaltool..." -ForegroundColor Cyan
    dotnet tool install -g dotnet-reportgenerator-globaltool
}

# 2. Clean previous test and coverage outputs
if (Test-Path $resultsDir) { Remove-Item $resultsDir -Recurse -Force }
if (Test-Path $reportDir)  { Remove-Item $reportDir  -Recurse -Force }

# 3. Run unit tests with Coverlet XPlat Code Coverage
Write-Host "Running unit tests and collecting coverage..." -ForegroundColor Cyan
dotnet test $solutionPath `
    --configuration Release `
    --collect:"XPlat Code Coverage" `
    --results-directory $resultsDir

if ($LASTEXITCODE -ne 0) {
    Write-Error "Tests failed. Aborting coverage report generation."
    exit $LASTEXITCODE
}

# 4. Generate Azure DevOps-style HTML report + console summary
Write-Host "Generating HTML and text coverage reports..." -ForegroundColor Cyan
reportgenerator `
    -reports:"$resultsDir/**/coverage.cobertura.xml" `
    -targetdir:"$reportDir" `
    -reporttypes:"HtmlInline_AzurePipelines;TextSummary"

# 5. Print coverage percentages directly to the terminal and open the HTML report
Get-Content "$reportDir/Summary.txt"
Start-Process "$reportDir/index.html"
