import configuration from "../config.json";

try {
  // eslint-disable-next-line @typescript-eslint/no-var-requires
  const configurationOverride = require("../config.override.json");
  Object.assign(configuration, configurationOverride);
  // eslint-disable-next-line no-empty
} catch (e) {}

export default configuration;