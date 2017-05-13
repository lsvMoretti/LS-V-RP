<?php

class DB {
	public static $Connection;
	
	public function __construct(){		//  	DB IP  			DB User DB Password   DB Name
		DB::$Connection = mysqli_connect(	"localhost", 	"root", 	"", 	"gamemode");
		if(DB::$Connection->connect_errno){
			die("Error connecting to database: ". $mysqli_connect_error);
		}
	}
}
new DB;
?>