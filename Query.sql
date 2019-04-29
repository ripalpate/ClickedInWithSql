declare @userId int = 2
declare @InterestName nvarchar(255) = 'Gardening'

Select * from Users;
Select * from UsersInterests;
Select * from Interests;

--Get Friends with same interest
Select u.Name, i.Name 
from UsersInterests as ui
Join Interests as i
ON ui.InterestId = i.Id AND i.Name = @InterestName
Join Users as u 
ON ui.UserId = u.Id AND u.Id != @userId;

--Delete user
Delete
From Users
Where id = @userId

-- Update user
declare @uId int = 1
Update Users
Set Name = 'Jimmy'
Where id = @uId

-- Update interest
declare @id int =7
Update Interests
Set Name = 'Movies'
Where id = @id;

Select * from Interests;
