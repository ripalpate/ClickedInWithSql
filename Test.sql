Select * from Users

Select * from Interests

Select * from Services;

Select u.* , InterestName = i.Name
from UsersInterests as ui
Join Interests as i
On i.Id = ui.InterestId
Join Users as u
On u.Id = ui.UserId
Order by u.Id


Select * from Users;
Select * from UsersService;
select * from UsersInterests;
Select * 
From Interests;

Select * from Services;







Select * from UsersService;


Select u.* , ServiceName = s.Name
from UsersService as us
Join Services as s
On s.Id = us.ServiceId
Join Users as u
On u.Id = us.UserId
Order by u.Id

Select u.*, ServiceName = s.Name, InterestName = i.Name
From Users as u
Join UsersService as us
On us.UserId = u.Id
Join Services as s
On s.Id = us.ServiceId
Join UsersInterests as ui
On ui.UserId = u.Id
Join Interests as i
On i.Id = ui.InterestId;

Select u.*, ServiceName = s.Name, InterestName = i.Name
From Users as u
Inner Join UsersService as us
On u.Id = us.UserId
Inner Join Services as s
On s.Id = us.ServiceId
Left Join UsersInterests as ui
On ui.UserId = u.Id
Left Join Interests as i
On i.Id = ui.InterestId;