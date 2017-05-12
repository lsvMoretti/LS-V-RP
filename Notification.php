<?php
    class Notification
    {
        function __construct($key, $value){
            $_SESSION["notification"][$key][] = $value;
        }

        static function Display(){
            if(isset($_SESSION["notification"])){
                foreach($_SESSION["notification"] as $name => $notification){
                    foreach($notification as $text){
                        echo("<div class='callout $name'><div class='row'><div class='medium-2 columns'>".($name == "alert" ? "<span class='fa fa-exclamation-circle fa-2x' style='color: #ff684c;'></span>" : "")."</div><div class='medium-10 columns'><p class='text-left middle'>$text</p></div></div></div>");
                    }
                }
            }
            unset($_SESSION["notification"]);
        }
    }
?>