# Azure Database Tools

## database export

This command exports your Azure database to a `.bacpac` file.

- `-v | --verbosity` - Optional. This argument can be used to change the verbose level of logging. Supported values are the ones defined by Microsoft's logging framework, therefore Trace, Debug, Information, Warning, Error and Critical. The default value is **Information**.

- `-e | --environment-name` - Optional. The value will be used to look for a file named `appsettings.{EnvironmentName}.json`. The default value is **null**, and the tool will look for `appsettings.json` if no value.

- `-c | --configuration-section` - Optional. Name of the configuration section where the connection string can be found. The default value is **`ConnectionStrings:Default`**.

## database clone

Executing this command will clone your database from Azure to your local environment. As the `database export`, this uses the `.bacpac` file method.

- `-v | --verbosity` - Optional. This argument can be used to change the verbose level of logging. Supported values are the ones defined by Microsoft's logging framework, therefore Trace, Debug, Information, Warning, Error and Critical. The default value is **Information**.

- `-e | --environment-name` - Optional. The value will be used to look for a file named `appsettings.{EnvironmentName}.json`. The default value is **null**, and the tool will look for `appsettings.json` if no value.

- `-d | --development-environment-name` - Optional. The value will be used to look for a file named `appsettings.{EnvironmentName}.json`. The default value is **Development**.

- `-c | --configuration-section` - Optional. Name of the configuration section where the connection string can be found. The default value is **`ConnectionStrings:Default`**.