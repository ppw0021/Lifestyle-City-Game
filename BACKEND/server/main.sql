-- Create a sequence (if it doesn't exist)
CREATE SEQUENCE IF NOT EXISTS building_instance_id_seq START 1;
CREATE SEQUENCE IF NOT EXISTS user_id_seq START 1;

CREATE TABLE "building_instances"(
    "instance_id" BIGINT DEFAULT nextval('building_instance_id_seq') NOT NULL,
    "structure_id" BIGINT NOT NULL,
    "x_pos" BIGINT NOT NULL,
    "y_pos" BIGINT NOT NULL
);
ALTER TABLE
    "building_instances" ADD PRIMARY KEY("instance_id");
CREATE TABLE "users"(
    "user_id" BIGINT DEFAULT nextval('user_id_seq') NOT NULL,
    "username" VARCHAR(255) NOT NULL,
    "sesh_token" VARCHAR(255) NULL,
    "level" BIGINT NOT NULL,
    "coins" BIGINT NOT NULL,
    "password" VARCHAR(255) NOT NULL,
    "xp" BIGINT NOT NULL
);
ALTER TABLE
    "users" ADD PRIMARY KEY("user_id");
ALTER TABLE
    "users" ADD CONSTRAINT "users_username_unique" UNIQUE("username");
ALTER TABLE
    "users" ADD CONSTRAINT "users_sesh_token_unique" UNIQUE("sesh_token");
CREATE TABLE "buildings_owner"(
    "base_owner_id" BIGINT NOT NULL,
    "building_instance_id" BIGINT NOT NULL
);
CREATE TABLE "building_prefabs"(
    "structure_id" BIGINT NOT NULL,
    "building_name" VARCHAR(255) NOT NULL,
    "x_width" BIGINT NOT NULL,
    "y_width" BIGINT NOT NULL,
    "max_count" BIGINT NOT NULL
);
ALTER TABLE
    "building_prefabs" ADD PRIMARY KEY("structure_id");
ALTER TABLE
    "building_instances" ADD CONSTRAINT "building_instances_structure_id_foreign" FOREIGN KEY("structure_id") REFERENCES "building_prefabs"("structure_id");
-- ALTER TABLE
--    "buildings_owner" ADD CONSTRAINT "buildings_owner_building_instance_id_foreign" FOREIGN KEY("building_instance_id") REFERENCES "building_instances"("instance_id");
--ALTER TABLE
--    "buildings_owner" ADD CONSTRAINT "buildings_owner_base_owner_id_foreign" FOREIGN KEY("base_owner_id") REFERENCES "users"("user_id");
-- Create prefab buildings
insert into building_prefabs (structure_id, building_name, x_width, y_width, max_count)
values
(0, 'Path', 1, 1, 99),		--FloorParent _Prefabs
(1, 'House', 1, 1, 9),		--House_02 BasAssetStore/LowPolyVillage/Prefabs
(2, 'Windmill', 1, 1, 99),	--Windmill BasAssetStore/LowPolyVillage/Prefabs
(3, 'Townhall', 1, 2, 99);	--House_03 BasAssetStore/LowPolyVillage/Prefabs

-- Create users
insert into users (username, password, sesh_token, level, coins, xp)
values 
('dec5star', 'declan02', 'abcdefg', 0, 999999, 0),
('jojo', 'bizzare51', 'aslkjdk', 0, 999999, 0),
('dio', 'brando69', 'adnngfd', 0, 999999, 0),
('demo1', 'apples', 'sadfjhf', 0, 750, 0),
('demo2', 'oranges', 'sadfytf', 0, 750, 0),
('demo3', 'kiwifruit', 'sawesdf', 0, 750, 0),
('demo4', 'bananas', 'sadftff', 0, 750, 0),
('demo5', 'mandarin', 'sadfjgf', 0, 750, 0),
('demo6', 'strawberries', 'sahssdf', 0, 750, 0);


-- Building instances
--insert into building_instances (structure_id, x_pos, y_pos)
--values
--(1, 0, 0),
--(1, 0, 0),
--(1, 0, 0),
--(2, 0, 1),
--(2, 0, 1),
--(2, 0, 1),
--(0, 3, 3),
--(0, 4, 4),
--(0, 5, 5);

-- Building ownership
--insert into buildings_owner (base_owner_id, building_instance_id)
--values
--(1, 1),
--(2, 2),
--(3, 3),
--(1, 4),
--(2, 5),
--(3, 6),
--(1, 7),
--(2, 8),
--(3, 9);