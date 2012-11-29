create procedure dbo.CheckAndInsertData
(
	@Raw_Xml xml
)
as
begin

select 
current_observation.value('(observation_time)[1]', 'varchar(50)') as ObsTime,
current_observation.value('(temp_f)[1]', 'float') as Temp_f,
current_observation.value('(temp_c)[1]', 'float') as Temp_c,
current_observation.value('(relative_humidity)[1]', 'float') as RelHum,
current_observation.value('(wind_mph)[1]', 'float') as Wind_mph ,
current_observation.value('(pressure_mb)[1]', 'float') as Pressure_mb,
current_observation.value('(dewpoint_f)[1]', 'float') as Dewpt_f
into #Staging
from 
@Raw_Xml.nodes('current_observation') as rawdata(current_observation)


;merge into Observations
using #Staging
on #Staging.ObsTime = Observations.ObsTime
when not matched then
insert (ObsTime,Temp_f,Temp_c,RelHum,Wind_mph,Pressure_mb,Dewpt_f)
values (ObsTime,Temp_f,Temp_c,RelHum,Wind_mph,Pressure_mb,Dewpt_f);

drop table #Staging


end
go