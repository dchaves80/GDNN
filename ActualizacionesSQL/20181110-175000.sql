USE [Sistema]
GO
/****** Object:  StoredProcedure [dbo].[GetTreatmentsBySucursal]    Script Date: 10/11/2018 17:49:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetTreatmentsBySucursal] (@UserId bigint)
AS
BEGIN
select * from Treatment where UserId = @UserId

END
