module.exports = function () {
  var instanceRoot = "C:\\websites\\Habitat.dev.local";
  var config = {
    websiteRoot: instanceRoot + "\\Website",
    frontendRoot: instanceRoot + "\\Website\\frontend",
    sitecoreLibraries: instanceRoot + "\\Website\\bin",
    licensePath: instanceRoot + "\\Data\\license.xml",
    solutionName: "Habitat",
    buildConfiguration: "Debug",
    buildToolsVersion: 15.0,
    buildMaxCpuCount: 0,
    buildVerbosity: "minimal",
    buildPlatform: "Any CPU",
    publishPlatform: "AnyCpu",
    runCleanBuilds: false
  };
  return config;
}
