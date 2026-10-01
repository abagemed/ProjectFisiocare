Imports Microsoft.VisualBasic
Imports system
Imports System.Xml.XPath
Imports System.Xml.Xsl
Imports System.Xml
Imports System.IO
Imports System.Security.Cryptography

imports System.Diagnostics
Imports System.Text


Public Class CFDIclass
    Function CadenaOriginal()

        ''Cargar el XML
        Dim reader As StreamReader = New StreamReader("C:\reporteFisio\archivosFac\cp\generaCadena.xml")
        Dim myXPathDoc As XPathDocument = New XPathDocument(reader)

        'Cargando el XSLT
        Dim myXslTrans As XslCompiledTransform = New XslCompiledTransform()
        myXslTrans.Load("C:\reporteFisio\archivosFac\cadenaoriginal_3_2.xslt")

        Dim str As StringWriter = New StringWriter()
        Dim myWriter As XmlTextWriter = New XmlTextWriter(str)

        ''Aplicando transformacion
        myXslTrans.Transform(myXPathDoc, myWriter)

        ''Resultado
        Dim resultado As String = str.ToString()
        Return resultado
    End Function

    Function encryptaSHA1(ByVal CadOriginal As String)
        Dim myString As String = CadOriginal
        Dim Data As Byte()

        Data = Encoding.ASCII.GetBytes(myString)

        Dim shaM As New SHA1Managed()
        Dim resultHash As Byte() = shaM.ComputeHash(Data)

        Dim resultHexString = CadOriginal
        Dim b As Byte

        For Each b In resultHash
            resultHexString += Hex(b)
        Next
        Dim b64 As String = Convert.ToBase64String(resultHash)
        Return b64
    End Function

    Sub firmaDoc(ByVal cadena As String)
        Dim objCert As New X509Certificates.X509Certificate2("C:\reporteFisio\archivosFac\fmd020730pq5_1307161754s.cer")
        'Dim objContent As  

    End Sub

    Function encryptaSHA1_2(ByVal CadOriginal As String)
        Dim sha1 As SHA1 = SHA1Managed.Create()
        Dim encoding As New ASCIIEncoding()
        Dim stream() As Byte

        Dim sb As New StringBuilder()
        stream = sha1.ComputeHash(encoding.GetBytes(CadOriginal))

        For i As Integer = 0 To stream.Length - 1
            sb.AppendFormat("{0:x2}", stream(i))
            i = i + 1
        Next i

        Return sb.ToString()
    End Function

    Function xxx(ByVal cadOriginal)
        Dim sha1Obj As New Security.Cryptography.SHA1CryptoServiceProvider
        Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(cadOriginal)

        bytesToHash = sha1Obj.ComputeHash(bytesToHash)

        Dim strResult As String = ""

        For Each b As Byte In bytesToHash
            strResult += b.ToString("x2")
        Next

        Return strResult

    End Function
End Class
