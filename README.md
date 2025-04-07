# Vue + .NET 8 + Hangfire 

## TEST WebAPI, WorkerService and Vue Grid

To run the application is necessary download the project at GitHub.

	https://github.com/quadradosimi/GermanyTest

Run Back-end (WebAPI)
	
	Open the .Net 8 project on Data.sln file, inside WebApi folder. Change the ConnectionStrings with your database settings, at file appsettings.json.
	Run the migration to build database structure with code below (set Data.Infra before run the script)

		add-migration [name]
		
	and after run

		update-database
		
	Choice Data.Web to start the software and run. The swagger will appear.
	
Run Back-end (WorkerService)
	
	Open the .Net 8 project on ApiHangFire.sln file, inside WorkerService folder. Change the ConnectionStrings with your database settings, at file appsettings.json.
	Create a new database call HangfireDB. Choice ApiHangFire to start the software and run. The swagger will appear. To test you just need run the GET Data endpoint 
	inside the swagger and the Job will run, get the external data and call the WebAPI to save it.		
		
Run Front-end

	The front-end application is inside folder WebVue/GridApp. Change SERVER_URL at the WebVue/GridApp/config.json with the right API url. 
	At the prompt in WebVue/GridApp folder and run codes below
	
		npm i
		
		and after
		
		npm run dev
		
	The locally server will run and show the url to set in browser. In the screen will appear a grid with the data.
