create procedure sp_openfile
@Id int 
as
begin
SELECT * FROM Documents WHERE DocumentId = @Id
end;

exec sp_openfile 2