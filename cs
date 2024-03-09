CREATE TABLE CachedSiteIDsForServices (
    ServiceListID INT,
    SiteID INT,
    CacheTimestamp DATETIME NOT NULL DEFAULT GETDATE(),
    PRIMARY KEY (ServiceListID, SiteID)
);

-----
DECLARE @ServiceListID INT = 1; -- Starting point

WHILE @ServiceListID <= 25 -- Assuming 25 is the maximum ServiceListID
BEGIN
    -- Clear existing entries for the current ServiceListID
    DELETE FROM CachedSiteIDsForServices WHERE ServiceListID = @ServiceListID;
    
    -- Populate cache for the current ServiceListID
    INSERT INTO CachedSiteIDsForServices (ServiceListID, SiteID)
    SELECT @ServiceListID, SiteID 
    FROM dbo.fn_Clinic_Search_Get_Site_IDs_With_Drug_Services(CONVERT(VARCHAR, @ServiceListID));
    
    SET @ServiceListID = @ServiceListID + 1;
END

----
SELECT SiteID 
FROM CachedSiteIDsForServices 
WHERE ServiceListID = @YourDesiredServiceListID;


