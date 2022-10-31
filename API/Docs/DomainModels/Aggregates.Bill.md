# Domain Models

## Bill

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "type": "1", // Other = 1, Rent, Gas, Eletricity, Trash, Phone, Internet, Water
  "isPaid": false,
  "serviceProviderName": "PGE",
  "Cost": {
    "price": 500,
    "currency": "zł" // zł, €
  },
  "DateOfPayment": "2021-01-01",
  "personId": "00000000-0000-0000-0000-000000000000"
}
```
