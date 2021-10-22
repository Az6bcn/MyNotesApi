CREATE TABLE [dbo].[Subscriptions] (
    [Id]             INT                IDENTITY (1, 1) NOT NULL,
    [SubscriptionId] VARCHAR (50)       NOT NULL,
    [Email]          VARCHAR (50)       NOT NULL,
    [CustomerId]     VARCHAR (50)       NOT NULL,
    [TrialExpired]   DATETIMEOFFSET (7) NOT NULL,
    [Currency]       VARCHAR (10)       NOT NULL,
    CONSTRAINT [PK_Subscriptions] PRIMARY KEY CLUSTERED ([Id] ASC)
);





