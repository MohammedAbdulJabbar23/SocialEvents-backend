postgresinit:
	docker run --name resturant-work -p 5434:5432 -e POSTGRES_USER=root -e POSTGRES_PASSWORD=password -d postgres:15-alpine

postgres:
	docker exec -it resturant-work psql 

postgresstart:
	docker start resturant-work
createdb: 
	docker exec -it resturant-work createdb --username=root --owner=root resturantdb
dropdb: 
	docker exec -it resturant-work dropdb resturantdb 

migratedb: 
	dotnet ef migrations add AddRestaurantTableConfig

.PHONY: postgresinit