# 目的
-----------------------------
為大量資料需求而設計ETL Process和WEB API提供統計資料
# 環境
----------------------------
- ASP.  NET 5
- MS SQL

# ETL流程圖
---------------------------
 ```
 實作專案：ETLProcess
 ```
![ETL](https://github.com/puffann1368/ETL_HW/blob/master/ETL_Process.png?raw=true)
### 說明
1. 使用Buffer達到非同步作業
2. 使用多執行緒處理大量資料解決處理時間過長問題


# Table Schema
---------------------------
![ETL](https://github.com/puffann1368/ETL_HW/blob/master/Schema.png?raw=true)

### 說明
1. 此規劃是假設可提供特定使用者每月使用量數據而設計,
使用db Sharding方式開12個table依月份作存取.
2. 每個月可將Ｎ個月前資料做搬移或刪除.


# WEB API
--------------------------
 ```
 實作專案：ETLAPI
 ```
### 1.Get lineItem/UnblendedCost grouping by product/productname
- URL
    ``
    [POST] http://{domainname}/ETL/GetSumUnblendedCost
    ``
- SPEC

    參數名稱    | 格式  |  
    ----------  |:-----:|
    AccountID   | String |  
    StartDate   | DateTime | 
    EndDate     | DateTime | 
    ```json
    {   
        "AccountID":"987654321",     
        "StartDate":"2020-04-01",
        "EndDate":"2020-04-30"
    }
    ```
### 2.Get daily lineItem/UsageAmount grouping by product/productname
- URL
    ``
    [POST] http://{domainname}/ETL/GetSumUsageAmount
    ``
- SPEC

    參數名稱    | 格式  |  
    ----------  |:-----:|
    AccountID   | String |  
    StartDate   | DateTime | 
    EndDate     | DateTime | 
    ```json
    {   
        "AccountID":"987654321",     
        "StartDate":"2020-04-01",
        "EndDate":"2020-04-30"
    }
    ```

### 說明:
1. 使用Json格式POST至API.
2. 因需依查詢月份動態select table,所以ORM還是選擇使用Dapper.
3. 使用Cache方式減少回應時間.
4. 第二個API使用Key-Value方式,先產生日期(Key),再依日期更新UsageAmount.
5. 建立 NonClustered Index Include column減少DB Lockup時間.
6. 可支援微服務.
