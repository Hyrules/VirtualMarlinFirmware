# Virtual RepRap Firmware
Requirement .Net Framework 4.7

A virtual reprap firmware for testing serial connections.

This is a virtual version of a reprap 3d printer. It will answer to some basic M commands :
You will need to connect it to some COM Port devices like BigTreeTech TFT screens
using a USB to TTL serial dongle. I created it to test my version of the TFT3.5 firmware 
instead of always having to upload to a real 3D printer. The application does not serve a comm
port by itself. It will only connect to one.

- M105
- M503
- M92
- M115
- M114
- M220
- M221
- G1

Any other command will be answered by ok.

Still a work in progress. 
