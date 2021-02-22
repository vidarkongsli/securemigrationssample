param (
    [Parameter(Mandatory)]
    [ValidatePattern('^[-\w\._\(\)]+$')]
    $deploymentName,
    [Parameter(Mandatory)]
    $resourceGroup,
    [Parameter(Mandatory=$false)]
    $location='norwayeast',
    [Parameter(Mandatory=$false)]
    [ValidateScript({test-path $_ -PathType Leaf})]
    $templateFile = "$PSScriptRoot\azuredeploy.json",
    [ValidateScript({test-path $_ -PathType Leaf})]
    $parameterFile = "$PSScriptRoot\azuredeploy.parameters.json"
)

$ErrorActionPreference = 'stop'

az group create -l $location -n $resourceGroup | out-null
$validationResult = az deployment group validate -g $resourceGroup --template-file $templateFile `
    --parameters @$parameterFile | ConvertFrom-Json

if (!$validationResult) {
    Write-Error "Az command failed!"    
}

if ($null -ne $validationResult.error) {
    $global:ARM_VALIDATION=$validationResult
    $validationResult | ConvertTo-Json
} else {
    Write-Output 'Validation OK.'
}
