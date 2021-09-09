db.createUser(
    {
        user: "m.parsa4",
        pwd: "123456",
        roles: [
            {
                role: "readWrite",
                db: "eWallet"
            }
        ]
    }
)

db = db.getSiblingDB('eWallet')

db.User.insertOne({ Username: 'm.parsa', Password: '123456' })
db.Client.insertOne(
    { "PhoneNumber": "09123456789", "NationalCode": "1234567891", "NationalCardSerial": "123456", "FirstName": "Mohammad", "LastName": "Parsa", "Username": "user-16036303889172", "BirthDate": "9/20/1986 12:00:00 AM", "NickName": "Mohi", "UserId": 167506, "FinancialLevel": { "Level": "USER_FINANCIAL_LEVEL_CELLPHONE_NATIONALCODE_VERIFIED", "LevelName": "APPROVED_BY_PHONE_NUMBER", "Value": 2 }, "Tokens": [{ "AccessToken": "11", "AccessTokenExpire": { "$date": "2024-01-01T00:00:00.000Z" }, "RefreshToken": "11", "RefreshTokenExpire": { "$date": "2024-01-01T00:00:00.000Z" }, "TokenAcquire": { "$date": "2020-01-01T00:00:00.000Z" }, "DeviceId": "09123456789", "KeyId": "948528034adaac01603623744", "CreateDate": { "$date": "2020-01-01T00:00:00.000Z" }, "ExpireDate": { "$date": "2024-11-21T13:44:51.345Z" } }], "BourseCode": "04216" }
)