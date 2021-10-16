CREATE TABLE [dbo].[Subscriptions] (
    [SubscriptionId] VARCHAR (50)       NOT NULL,
    [Email]          VARCHAR (50)       NOT NULL,
    [CustomerId]     VARCHAR (50)       NOT NULL,
    [TrialExpired]   DATETIMEOFFSET (7) NOT NULL
);

