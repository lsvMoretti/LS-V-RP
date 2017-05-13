<?php
	// First, include the config, which initializes the database connection
	require_once 'config.php';
	require_once 'Notification.php';
	// Escape the email, secure from SQL injection
	$email = DB::$Connection->escape_string($_POST["Email_Address"]);
	
	// Escape the password, secure from SQL injection, encrypt it
	$encrypted_password = password_hash(DB::$Connection->escape_string($_POST["Password"]), PASSWORD_BCRYPT);
	
	// Insert user into database
	DB::$Connection->query("INSERT INTO users(email, hash) VALUES('$email','$encrypted_password')");
	
	if(DB::$Connection->errno){
		switch(DB::$Connection->errno){
			case 1062:
				new Notification("error","Account with this email already exists");
			break;
		}
	}
	else{
		new Notification("success", "You have succssfully registered");
	}
	Header('Location: /ucpregister.php');
?>