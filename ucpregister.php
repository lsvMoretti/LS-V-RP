<?php

require_once 'Notification.php';
?>
<!doctype html>
<html lang="en">
    <head>
        <meta charset="utf-8">
        <title></title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <link href="css/bootstrap.css" rel="stylesheet" type="text/css" media="all" />
        <link href="css/stack-interface.css" rel="stylesheet" type="text/css" media="all" />
        <link href="css/theme-charcoal.css" rel="stylesheet" type="text/css" media="all" />
        <link href="css/custom.css" rel="stylesheet" type="text/css" media="all" />
        <link href="https://fonts.googleapis.com/css?family=Open+Sans:200,300,400,400i,500,600,700" rel="stylesheet">
        <link href="https://fonts.googleapis.com/css?family=Karla:400,400i,700,700i" rel="stylesheet" type="text/css">
        <link href="css/font-karla.css" rel="stylesheet" type="text/css">

    </head>
    <body data-smooth-scroll-offset="77">
        <div class="nav-container"> </div>
        <div class="main-container">
            <section class="imageblock switchable feature-large height-100">
                <div class="imageblock__content col-md-6 col-sm-4 pos-right">
                    <div class="background-image-holder"> <img alt="image" src="img/10_gtavpc_03272015.jpg"> </div>
                </div>
                <div class="container pos-vertical-center">
                    <div class="row">
                        <div class="col-md-5 col-sm-7">
                            <h2>Create a UCP Account</h2>
                            <p class="lead">Get started with a UCP account today to play on the server!</p>
                            <form action="register.php" method="post">
                                <div class="row">
									<?php Notification::Display(); ?>
                                    <div class="col-xs-12"> <input type="email" name="Email Address" placeholder="Email Address"> </div>
                                    <div class="col-xs-12"> <input type="password" name="Password" placeholder="Password"> </div>
                                    <div class="col-xs-12"> <button type="submit" class="btn btn--primary type--uppercase">Create Account</button> </div>
                                    <div class="col-xs-12"> <span class="type--fine-print">By signing up, you agree to the <a href="#">Terms of Service</a></span> </div>
                                </div>
                            </form>
                        </div>
                    </div>
            </section>
        </div>
        <script src="js/jquery-3.1.1.min.js"></script>
        <script src="js/parallax.js"></script>
        <script src="js/countdown.min.js"></script>
        <script src="js/smooth-scroll.min.js"></script>
        <script src="js/scripts.js"></script>

    </body>

</html>