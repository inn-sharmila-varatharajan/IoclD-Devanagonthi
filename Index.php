<?php 
  $ip = $_SERVER['REMOTE_ADDR']; //get the client's IP address 
  $client = $_SERVER['HTTP_USER_AGENT']; //get client's HTTP ID 
  $today = date("Ymd H:i:s"); //get the current date and time 
  $f = fopen("log.csv","a"); //open file to add data 
  $param = $_REQUEST['a']; //get the value of the sent variable "a" 
  fwrite($f,"$today; $ip; $client; sensor=$param\r\n-----------------\ r\n"); //writing data to file 
  fclose($f); //close the file 
?> 

<p>GPRS data read page</p>