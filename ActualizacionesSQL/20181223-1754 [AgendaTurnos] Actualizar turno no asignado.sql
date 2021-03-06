USE [Sistema]
GO
/****** Object:  StoredProcedure [dbo].[Update_Cliente]    Script Date: 23/12/2018 17:41:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Turno] 
(
@DateReservacion datetime,
@IdTurno bigint,
@IdUsuario bigint,
@Estado varchar(20),
@IdBox bigint
)
as
begin
	update AgendaTurnos set 
	DateReservacion = @DateReservacion, 
	Estado = @Estado,
	IdBox = @IdBox
	where Id=@IdTurno and IdUsuario = @IdUsuario
end


