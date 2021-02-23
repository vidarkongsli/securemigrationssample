param (
    [Parameter(Mandatory)]
    [ValidatePattern('^[-\w\._\(\)]+$')]
    $deploymentName,
    [Parameter(Mandatory)]
    $resourceGroup,
    [Parameter(Mandatory)]
    $sqlAdminPassword,
    [Parameter(Mandatory)]
    $defaultConnectionString,
    [Parameter(Mandatory=$false)]
    $location='westeurope',
    [Parameter(Mandatory=$false)]
    [ValidateScript({test-path $_ -PathType Leaf})]
    $templateFile = "$PSScriptRoot\azuredeploy.json",
    [ValidateScript({test-path $_ -PathType Leaf})]
    $parameterFile = "$PSScriptRoot\azuredeploy.parameters.json"
)
$ErrorActionPreference = 'stop'

az group create -l $location -n $resourceGroup | out-null
$result = az deployment group create -g $resourceGroup -n $deploymentName `
    --template-file $templateFile --parameters @$parameterFile --parameters "sqlAdminPassword=`"$sqlAdminPassword`"" "defaultConnectionString=`"$defaultConnectionString`"" `
    | ConvertFrom-Json
if (!$result) {
    Write-Error "Az command failed!"    
}
$result