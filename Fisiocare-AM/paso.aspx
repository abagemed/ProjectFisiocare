<%@ Page Language="VB" AutoEventWireup="false" CodeFile="paso.aspx.vb" Inherits="paso" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body onload="javascript:if(history.length>0)history.go(+1)">
    <form id="form1" runat="server">
    <div>
        <cc1:messagebox id="Messagebox1" runat="server"></cc1:messagebox>
    
    </div>
    </form>
</body>
</html>
