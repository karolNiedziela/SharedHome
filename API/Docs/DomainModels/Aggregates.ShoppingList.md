# Domain Models

## Shopping list

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "name": "List name",
  "isDone": false,
  "products": [
    {
      "id": "00000000-0000-0000-0000-000000000000",
      "name": "Product name",
      "quantity": 0,
      "price": {
        "price": 10,
        "currency": "zł" // zł, €
      },
      "netContent": {
        "netContent": 5,
        "netContentType": 3 // kg = 1, dag, g, l, ml
      },
      "isBought": true
    },
    {
      "id": "00000000-0000-0000-0000-000000000000",
      "name": "Product name",
      "quantity": 0,
      "price": {
        "price": 5,
        "currency": "zł" // zł, €
      },
      "netContent": {
        "netContent": 2,
        "netContentType": 1 // kg = 1, dag, g, l, ml
      },
      "isBought": false
    }
  ],
  "personId": "00000000-0000-0000-0000-000000000000",
  "createdAt": "2021-01-01T00:00:00.0000000Z",
  "modifiedAt": "2021-01-01T00:00:00.0000000Z"
}
```
