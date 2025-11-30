## 專案基本資訊
此專案為已有前台基本版面的專案，增設後臺管理介面及增補前台部分功能。

- **目標框架**: .NET Framework 4.8
- **資料庫**: 使用 Entity Framework 進行資料存取，支援 SQL Server 和 LocalDb。


### SMTP
- 在 `Web.config` 的 `<appSettings>` 中配置 `SmtpServer` 和 `google2FAAppPw`。

### Google reCAPTCHA
- 在 `Web.config` 的 `<appSettings>` 中配置 `SiteKey` 和 `SecretKey`。

### Google map
- 在 `Web.config` 的 `<appSettings>` 中配置 `gmapSiteKey` 。



## 注意事項
- 請勿將敏感資訊（如 API 金鑰、密碼）直接提交到版本控制系統。 `(待修改)`
