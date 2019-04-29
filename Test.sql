Select * from Users

Select * from Interests;

Select * from Services;

Select * from UsersService;


--Getting Services for users
Select u.* , ServiceName = s.Name
from UsersService as us
Join Services as s
On s.Id = us.ServiceId
Join Users as u
On u.Id = us.UserId
Order by u.Id

--Getting Users with Interests
Select u.*, InterestName = i.Name
From UsersInterests as ui
Join Interests as i
On i.Id = ui.InterestId
Join Users as u
On u.Id = ui.UserId
Order By u.Id

