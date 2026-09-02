$solution = "HelloWorldApi.sln"

function Run-Step {
    param(
        [string]$Name,
        [scriptblock]$Command
    )

    Write-Host "==> $Name"
    & $Command

    if ($LASTEXITCODE -ne 0) {
        Write-Host "==> FAILED: $Name"
        exit $LASTEXITCODE
    }
}

Run-Step "Format check" { dotnet format $solution --verify-no-changes }
Run-Step "Build" { dotnet build $solution --configuration Release }
Run-Step "Tests" { dotnet test $solution --configuration Release --no-build }

Write-Host "==> Quality gate passed"