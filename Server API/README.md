# Siememes Backend API

## Error Code

0 No error. Success.

1 Generic error.

2 Data not found.

10 Unauthorized Error.

## Rest API

**Auth required** : YES, Auth code

**Auth URL** : TBD

**Response Code** : 200 OK

**Shared Output**: All responses adhere to the following format:

```json
  {
    "errorCode": 0,
    "message": "Error message or success message",
    "data": {
        // Output data if specific
    }
  }
```

### Login

**URL** : `/api/login/`

**Method** : `POST`

**Input** : The login information from a third party, where the ID can be any string.
```json
{
    "id": "abc@xyz.com",
    "name": "test account",
    "screenName": "jimkao0126",
    "profileImageUrl": "https://pbs.twimg.com/profile_images/1574405532903153664/TC6kpsRx_normal.png"
}
```

**Output** :  Provide the authentication code. This code must be included in the Authentication header.

```json
{
    "errorCode": 0,
    "message": ""
    "data":
    {
        "authCode": "303df1267cf74ff5b33ab72b0c97ea55",
        "expired": 1715693033
    }
}
```

### Get player Info

**URL** : `/api/UserInfo/`

**Method** : `Get`

**Output** :  Retrieve player information using the authentication code. The player information is obtained based on the provided auth code.

```json
{
    "userInfo": "{ \"id\": 452, \"name\": \"Emily\", \"age\": 27, \"isEmployee\": true, \"department\": \"Sales\", \"salary\": 75000.50, \"skills\": [\"communication\", \"presentation\", \"negotiation\"], \"startDate\": \"2021-06-15\", \"activeProjects\": 5, \"email\": \"emily.johnson@example.com\" }",
    "errorCode": 0,
    "message": ""
}
```


### Update player Info

**URL** : `/api/UserInfo/`

**Method** : `POST`

**Input** : The userInfo must be provided as a JSON format string. No content validation is performed. The player information is retrieved based on the provided authentication code.

```json
{
  "userInfo": "{ \"id\": 452, \"name\": \"Emily\", \"age\": 27, \"isEmployee\": true, \"department\": \"Sales\", \"salary\": 75000.50, \"skills\": [\"communication\", \"presentation\", \"negotiation\"], \"startDate\": \"2021-06-15\", \"activeProjects\": 5, \"email\": \"emily.johnson@example.com\" }"
}
```

**Output** : The player information is returned in the same JSON format as the input.

```json
{
    "userInfo": "{ \"id\": 452, \"name\": \"Emily\", \"age\": 27, \"isEmployee\": true, \"department\": \"Sales\", \"salary\": 75000.50, \"skills\": [\"communication\", \"presentation\", \"negotiation\"], \"startDate\": \"2021-06-15\", \"activeProjects\": 5, \"email\": \"emily.johnson@example.com\" }",
    "errorCode": 0,
    "message": ""
}
```


### Player retrieve chest boxes

**URL** : `/api/PlayerChest/`

**Method** : `Get`

**Output** :  Retrieve player chest boxes. The player information is obtained based on the provided auth code.

```json
{
    "chestBoxes": [
        {
            "slotId": 1,
            "starttime": 0,
            "Endtime": 0,
            "isSeal": false,
            "items": [],
            "buffId": 0,
            "isSteal": false
        }
    ],
    "errorCode": 0,
    "message": ""
}
```


### Player receive a new chest

**URL** : `/api/UserInfo/`

**Method** : `POST`

**Input** : The player purchases or acquires a new chest. The slot ID must be greater than or equal to 0. The player information is retrieved based on the provided authentication code.

```json
{
  "chestDataId": 21021,
  "slotId": 1
}
```

**Output** : Return all of the player's chest boxes, including the newly acquired one.

```json
{
    "chestBoxes": [
        {
            "slotId": 1,
            "starttime": 0,
            "Endtime": 0,
            "isSeal": false,
            "items": [],
            "buffId": 0,
            "isSteal": false
        }
    ],
    "errorCode": 0,
    "message": ""
}
```


### Player chest utilities

**URL** : `/api/UserInfo/`

**Method** : `POST`

**Input** : The player can perform the following actions: put an item into a chest box, seal the chest box, or apply a buff to the chest box. While these actions can be done in a single API call, it is recommended to handle them separately. Set itemId to 0 or omit it if no item is to be placed, and do the same for buffId. Set sealChestBox to false or omit it if the chest box should not be sealed. Player information is retrieved using the provided authentication code.

```json
{
  "slotId": 1,
  "itemId": 3,
  "sealChestBox": true,
  "buffId": 5002
}
```

**Output** : Return all of the player's chest boxes, including the one that was just modified.

```json
{
    "chestBoxes": [
        {
            "slotId": 1,
            "starttime": 1726416495,
            "Endtime": 1726459695,
            "isSeal": true,
            "items": [
                3,
                3
            ],
            "buffId": 5002,
            "isSteal": false
        }
    ],
    "errorCode": 0,
    "message": ""
}
```

### Retrieve target user and chest information

**URL** : `/api/StealChest/`

**Method** : `Get`

**Output** : Retrieve the sealed chest boxes of other players. The player information is obtained based on the provided auth code.

```json
{
    "userChestDatas": [
        {
            "userGuid": "0a973ccb-5545-4fe7-9a88-9f5e5ca3a821",
            "chestBoxes": [
                {
                    "slotId": 1,
                    "chestDataId": 21011,
                    "starttime": 1726414395,
                    "endtime": 1726457595,
                    "isSeal": true,
                    "items": [
                        1,
                        1,
                        2
                    ],
                    "buffId": 0,
                    "isSteal": true
                }
            ]
        },
        {
            "userGuid": "59fc69da-17f3-4675-97c6-0275b3a0c26c",
            "chestBoxes": [
                {
                    "slotId": 0,
                    "chestDataId": 21011,
                    "starttime": 1726844889,
                    "endtime": 1726888089,
                    "isSeal": true,
                    "items": [
                        2
                    ],
                    "buffId": 0,
                    "isSteal": false
                },
                {
                    "slotId": 1,
                    "chestDataId": 21011,
                    "starttime": 1726844891,
                    "endtime": 1726845229,
                    "isSeal": true,
                    "items": [
                        3
                    ],
                    "buffId": 0,
                    "isSteal": false
                },
                {
                    "slotId": 2,
                    "chestDataId": 21011,
                    "starttime": 1726806123,
                    "endtime": 1726827723,
                    "isSeal": true,
                    "items": [
                        3
                    ],
                    "buffId": 0,
                    "isSteal": false
                }
            ]
        }
    ],
    "errorCode": 0,
    "message": ""
}
```


### Steal the target chest

**URL** : `/api/StealChest/`

**Method** : `POST`

**Input** : The target user guid and slot Id. Player information is retrieved using the provided authentication code.

```json
{
    "userGuid": "f432d04e-e507-4ca5-bd73-17d007351fa7",
    "slotId": 1
}
```

**Output** : Return the stolen chest information along with an error code of 0 if the theft is successful. If the target chest cannot be stolen, return an error code of 2. If the chest box contains a buff, return the buff ID as the error code (e.g., 5001 or 5003). For any internal errors, return an appropriate error code.

```json
{
    "userChestDatas": [
        {
            "userGuid": "f432d04e-e507-4ca5-bd73-17d007351fa7",
            "chestBoxes": [
                {
                    "slotId": 1,
                    "chestDataId": 21011,
                    "starttime": 1726414395,
                    "endtime": 1726457595,
                    "isSeal": true,
                    "items": [
                        1,
                        1,
                        2
                    ],
                    "buffId": 0,
                    "isSteal": true
                }
            ]
        }
    ],
    "errorCode": 0,
    "message": ""
}
```
