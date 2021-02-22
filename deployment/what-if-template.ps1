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
az deployment group what-if -g $resourceGroup -n $deploymentName --template-file $templateFile --parameters @$parameterFile
