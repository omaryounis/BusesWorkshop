<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BusesWorkshop.Pages.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>الشاشة الرئيسية</title>
    
        <!-- App css -->
        <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="../assets/css/core.css" rel="stylesheet" type="text/css" />
        <link href="../assets/css/components.css" rel="stylesheet" type="text/css" />
        <link href="../assets/css/icons.css" rel="stylesheet" type="text/css" />
        <link href="../assets/css/pages.css" rel="stylesheet" type="text/css" />
        <link href="../assets/css/menu.css" rel="stylesheet" type="text/css" />
        <link href="../assets/css/responsive.css" rel="stylesheet" type="text/css" />
        <link href="../assets/css/rtl.css" rel="stylesheet" type="text/css" />

        <!-- HTML5 Shiv and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
        <![endif]-->

        <script src="../assets/js/modernizr.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
   <section>
            <div class="container-alt">
                <div class="row">
                    <div class="col-sm-12">

                        <div class="wrapper-page">

                            <div class="m-t-40 account-pages">
                                <div class="text-center account-logo-box">
                                    <h2 class="text-uppercase text-center">
                                        <a href="index.html" class="text-success">
                                            <span><img src="../assets/images/login.png" alt="" height="50"></span>
                                        </a>
                                    </h2>
                                    <!--<h4 class="text-uppercase font-bold m-b-0">Sign In</h4>-->
                                </div>
                                <div class="account-content">

                                        <div class="col-xs-12">
                                            <div class="form-group ">
                                              <asp:TextBox ID="txtName" class="form-control" runat="server" required="" placeholder="اسم المستخدم"></asp:TextBox>
                                            </div>
                                        </div>
                                            <div class="clearfix"></div>
                                        <div class="col-xs-12">
                                            <div class="form-group">
                                                <asp:TextBox class="form-control" ID="txtPassword" runat="server" required="" placeholder="كلمة المرور" TextMode="Password" ></asp:TextBox>
                                            </div>
                                        </div>
                                            <div class="clearfix"></div>

                                        <div class="form-group text-center m-t-10">
                                            <div class="col-sm-12">
                                                <a href="#" class="text-muted" dir="rtl"> <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></a>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="form-group text-center">
                                            <div class="col-xs-12">
                                               <asp:Button class="btn w-md btn-bordered btn-danger waves-effect waves-light" ID="btnLogin" runat="server" Text="دخول" onclick="btnLogin_Click"   />
                                              
                                            </div>
                                        </div>


                                    <div class="clearfix"></div>

                                </div>
                            </div>
                            <!-- end card-box-->


                       

                        </div>
                        <!-- end wrapper -->

                    </div>
                </div>
            </div>
          </section>
   
     
    
        

    </form>
</body>
</html>
