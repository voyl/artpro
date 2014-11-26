MOJO RELEASE NOTES
------------------
There are three primary source files of interest. These are:

O CSTA.CS  - This is the CSTA32.DLL C# PInvoke wrappers
O TSAPI.CS - This is a group of methods that help hide the complexity of working with TSAPI from the developer
O FRMMOJOMAIN.CS - This is the main file for a sample application that presents the user with a virtual two-line telephone

There is an APP.CONFIG file that contains several string values referenced by FRMMOJOMAIN.CS. These are the TServer name, user
login ID, user password, and whether or not to enable the Outlook COM reference to present Outlook Contact screen pops to the
user if an incoming caller ID matches.

A few caveats I have noticed during testing the Mojo sample application.

O If an Outlook Contact record doesn't match the incoming caller ID the GUI hangs a few seconds before repainting itself.
O Juggling mulitple calls with hold and retrieve methods doesn't produce consistent results. This is apparently due to glitches
within the PBX system itself, as TServer error codes confirm this.
O Not all PBX systems provide service for status queries, such as Message Waiting Indicator (MWI) status. I have commented this
portion out since my test PBX system doesn't provide service.

