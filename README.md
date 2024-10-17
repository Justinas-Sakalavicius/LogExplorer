# Introduction
Console application that would parse a log csv file and print out logs based on a custom query.

## TODO

- [x] search any column by full string or partial string.
- [x] Query provided by user input
- [x] Format as your choice in example: [column_name = 'search_string']
- [x] Results from the query should be combined and returned in JSON format
- [x] Please provide project folder structure.

### Bonus: 
- [ ] Add Boolean logical operator support in the queries (AND, OR, NOT...);
- [x] Add multiple file support;
- [x] Add log count value in the resulting JSON output;

### More bonus points:

- [ ] Improvements are welcome 😊. For example, Deal with duplicates, combine results in a more readable format.
- [ ] Implement database layer. Save results in a database of your choice.
- [ ] Implement options to send alerts based on the severity of the log. No need to actually send the notification, you can use Console.WriteLine().
- [ ] Dynamic log parsing. Logs can have different columns, how would you attend to this?

### JSON Example (Result)
```
{
	"searchQuery": "deviceVendor='Microsoft'",
	"result": [{
			"deviceVendor": "Microsoft",
			"deviceProduct": "Windows Vista",
			"deviceVersion": 1,
			"signatureId": "Microsoft-Windows-Security-Auditing:4896",
			"severity": 5,
			"name": "One or more rows have been deleted from the certificate database.",
			"start": "2022-05-29T04:53:43.560000Z",
			"rt": "1653800023.56",
			"msg": "One or more rows have been deleted from the certificate database.",
			"shost": "DESKTOP-FQYFQMDY9RGD",
			"smac": "60:6C:66:8a:4c:a5",
			"dhost": "w2019r2008-srv",
			"dmac": "38:00:25:b4:b0:3f",
			"suser": "RIesa",
			"suid": null,
			"externalId": 4896,
			"cs1Label": "payload",
			"cs1": "",
			"sproc": ""
		},
		{
			"deviceVendor": "Microsoft",
			"deviceProduct": "Windows Vista",
			"deviceVersion": 1,
			"signatureId": "Microsoft-Windows-Security-Auditing:4624",
			"severity": 3,
			"name": "An account was successfully logged on.",
			"start": "2022-05-29T04:54:16.359000Z",
			"rt": "1653800056.359",
			"msg": "This event is generated when a logon session is created.",
			"shost": "LENOVO-TZJUVSD7H",
			"smac": "E4:A7:A0:65:60:dd",
			"dhost": "w2014r2010-srv",
			"dmac": "D8:3B:BF:0d:02:5a",
			"suser": "LENOVO-TZJUVSD7H$",
			"suid": 5e+188,
			"externalId": 4624,
			"cs1Label": "payload",
			"cs1": "",
			"sproc": ""
		}
	]
}
