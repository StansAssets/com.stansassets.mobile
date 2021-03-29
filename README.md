# Stans Assets Mobile Project
The package is a little helper that makes communication between Unity and native platforms(iOS right now) easier and more convenient. It allows you to send and get a callback and transfer your data between unity and native code in a more convenient way, and efficient way.

For example, if you have some image that you want to use in Unity and get from the iPhone gallery, you don't need to send all data as an array of char bytes to unity, you can just save this data into a buffer which this package provides, this will return a hash for it, and then you just can get this data by this hash where you want. Under the hood, it will get back data from the buffer, get a pointer to it and size, and then just send this pointer to Unity, where it'll create an array of bytes, fill it with data from the pointer, and then will return this data to you.

[![NPM Package](https://img.shields.io/npm/v/com.stansassets.ios-deploy)]()
[![openupm](https://img.shields.io/npm/v/com.stansassets.xcode-project?label=openupm&registry_uri=https://package.openupm.com)]()
[![Licence](https://img.shields.io/npm/l/com.stansassets.ios-deploy)](https://github.com/StansAssets/com.stansassets.mobile/blob/main/LICENSE.md)
[![Issues](https://img.shields.io/github/issues/StansAssets/com.stansassets.ios-deploy)](https://github.com/StansAssets/com.stansassets.mobile/issues)
