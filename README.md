# ED:Bindings

This is a little windows application that reads the key bindings file
so that it can be searchable and mappable to keys.

<img src="https://raw.githubusercontent.com/ghorsey/EdBindings/main/assets/edbindings.screenshot.gif">

## Features

* Support mapping device codes in Elite to actual labels of device (For example [X56 Bindings](https://www.edrefcard.info/device/SaitekX56).
* Allows filtering bindings.

## Device Mapping Files
1. The device mapping files are found in the `DeviceMappings` folder.
2. The files are in JSON format using the following Schema:

```
{
  "name": "{{Friendly Device Name, will appear in menu}}", 
  "controls": [
    {
      "deviceId": "{{The device from the ED binds file}}",
      "deviceName": "{{Friendly Device Name to show in the table}}",
      "controlLabel": "{{Friendly name for the key to show in the table}}",
      "controlValue": "{{The key value from the ED binds file}}"
    },
    // additional controls
  ]
}
```

See [X56.json](https://github.com/ghorsey/EdBindings/blob/main/src/EdBindings/DeviceMappings/X56.json) for a complete example.

## Please Send a PR with additional Devince Mapping Files
Please open a PR or start a discussion with your mappings

## Credits

* App Icon made by [Nikita Golubev](https://www.flaticon.com/authors/nikita-golubev) from [www.flaticon.com](https://www.flaticon.com/)
