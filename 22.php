<?php
//1. 连接数据库
mysql_query("set names 'gb2312'")
$db_host="45.207.178.240:3306";
$db_name="a202302200932018";
$db_pwd="Zf88888888";
$link=mysqli_connect($db_host,$db_name,$db_pwd);

if($link){
	echo "success";
}else{
	echo "fail";
}