# Gateways Task:

This sample project is managing gateways - master devices that control multiple peripheral devices. 
Your task is to create a REST service (JSON/HTTP) for storing information about these gateways and their associated devices. This information must be stored in the database. 
When storing a gateway, any field marked as “to be validated” must be validated and an error returned if it is invalid. Also, no more that 10 peripheral devices are allowed for a gateway.
The service must also offer an operation for displaying information about all stored gateways (and their devices) and an operation for displaying details for a single gateway. Finally, it must be possible to add and remove a device from a gateway.

Each gateway has:
•	a unique serial number (string), 
•	human-readable name (string),
•	IPv4 address (to be validated),
•	multiple associated peripheral devices. 

Each peripheral device has:
•	a UID (number),
•	vendor (string),
•	date created,
•	status - online/offline.

# Installation Guides

1.  Open the Gateways.sln file with Visual Studio
2.  Start the solution with API and UI running
3.  API should run on port 1337 and UI on port 1338 //if this is not the case please change the axios calls in Home.vue component //No automated build so stuff like this might happen :(
4.  Follow the UI to enter appropriate data into the database.
  4.1.  If you want to use postman instead you are welcome, at the bottom of the readme I will give an examples of to add
5.  Enjoy the application
  
  # Postman Examples
  
  ## Add Gateway
  ```
  {
        "hrname": "Something1",
        "ipv4": "1.1.1.1"
  }
  ```
  ## Add Peripheral
  ```
        {
            "vendor": "Huawei inc.",
            "dateCreated":"2012-04-23T18:25:43.511Z",
            "Status":1,
            "gatewaySerialNumber": "Serial0"
        }
  ```
  
  
