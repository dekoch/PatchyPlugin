Imports System.Xml
Imports System.Xml.Serialization
Imports System.IO

Module XML

    Dim xmldoc As New XmlDocument
    Dim xmlnode As XmlNode
    Public strOldPath As String = ""


    Public Function readXML(ByVal root As String, ByVal node As String, ByVal attribute As String, ByVal type As String, ByVal path As String)

        Dim strRead As String

        If path <> strOldPath Then
            xmldoc.Load(path)
            strOldPath = path
        End If

        xmlnode = xmldoc.SelectSingleNode(root)

        Try
            strRead = xmlnode.SelectSingleNode(node).Attributes(attribute).Value()
        Catch
            If node <> "version" Then
                errorXML(path & " " & node & ":" & attribute)
            End If

            Select Case type

                Case "boolean"
                    Return False

                Case "integer"
                    Return 0

                Case "string"
                    Return ""

                Case "double"
                    Return 0.0

                Case "single"
                    Return 0.0

                Case Else
                    Return ""

            End Select

        End Try

        Select Case type

            Case "boolean"
                Select Case strRead

                    Case "True"
                        Return True

                    Case "False"
                        Return False

                    Case Else
                        errorXML(path & " " & node & ":" & attribute & "(" & type & ")" & "=" & strRead)
                        Return ""

                End Select

            Case "integer"
                Dim str As New System.Text.StringBuilder
                Dim int As New System.Text.StringBuilder

                For Each c As Char In strRead
                    If Char.IsNumber(c) Then int.Append(c)
                    If Not Char.IsNumber(c) Then str.Append(c)
                Next

                If str.Length > 0 Or int.Length = 0 Then
                    errorXML(path & " " & node & ":" & attribute & "(" & type & ")" & "=" & strRead)
                End If

                Return int.ToString

                str = Nothing
                int = Nothing

            Case "string"
                If strRead.Length > 65535 Then
                    errorXML(path & " " & node & ":" & attribute & "(" & type & ")" & "=" & strRead)
                End If

                Return strRead

            Case "double"
                Dim str As New System.Text.StringBuilder
                Dim int As New System.Text.StringBuilder
                Dim comma As New System.Text.StringBuilder
                Dim dbl As New System.Text.StringBuilder

                For Each c As Char In strRead
                    If Char.IsNumber(c) Or InStr(c, ",") Then
                        dbl.Append(c)
                    Else
                        str.Append(c)
                    End If

                    If Char.IsNumber(c) Then int.Append(c)
                    If InStr(c, ",") Then comma.Append(c)
                Next

                If str.Length > 0 Or int.Length < 2 Or comma.Length <> 1 Then
                    errorXML(path & " " & node & ":" & attribute & "(" & type & ")" & "=" & strRead)
                End If

                Return dbl.ToString

                str = Nothing
                int = Nothing
                comma = Nothing
                dbl = Nothing


            Case "single"

                Dim bol As Boolean = False

                Dim str0 As New System.Text.StringBuilder
                Dim str1 As New System.Text.StringBuilder

                For Each c As Char In XML.readXML(root, node, attribute, "string", path)

                    If bol = False Then
                        If Char.IsNumber(c) Then str0.Append(c)
                        If Not Char.IsNumber(c) Then bol = True
                    Else
                        If Char.IsNumber(c) Then str1.Append(c)
                    End If

                Next

                Return Convert.ToSingle(str0.ToString & "," & str1.ToString)

                bol = Nothing
                str0 = Nothing
                str1 = Nothing


            Case Else
                Return ""

        End Select

        strRead = Nothing

    End Function


    Public Sub writeXML(ByVal root As String, ByVal node As String, ByVal attribute As String, ByVal value As String, ByVal type As String, ByVal path As String)


            Dim bolPassed As Boolean = False


            Select Case type

                Case "boolean"
                    Select Case value

                        Case True
                            bolPassed = True

                        Case False
                            bolPassed = True

                        Case Else
                            bolPassed = False

                    End Select


                Case "integer"
                    Dim str As New System.Text.StringBuilder
                    Dim int As New System.Text.StringBuilder

                    For Each c As Char In value
                        If Char.IsNumber(c) Then int.Append(c)
                        If Not Char.IsNumber(c) Then str.Append(c)
                    Next

                If str.Length = 0 Then
                    bolPassed = True

                    If int.Length = 0 Then
                        value = 0
                    Else
                        value = int.ToString
                    End If

                Else
                    bolPassed = False
                End If


                Case "string"
                    bolPassed = True


                Case Else
                    errorXML("not supported type " & type)

            End Select


        If bolPassed = True Then

            If path <> strOldPath Then
                xmldoc.Load(path)
                strOldPath = path
            End If

            xmlnode = xmldoc.SelectSingleNode(root)

            Dim strOldWrite As String = xmlnode.SelectSingleNode(node).Attributes(attribute).Value()

            If value <> strOldWrite Then
                xmlnode.SelectSingleNode(node & "[@" & attribute & "='" & strOldWrite & "']/@" & attribute).Value = value
                xmldoc.Save(path)
            End If

            strOldWrite = Nothing

        Else

            errorXML(path & " " & root & "/" & node & ":" & attribute & "(" & type & ")" & "=" & value)

        End If

        bolPassed = Nothing

    End Sub

    Private Sub errorXML(ByVal strError As String)
        MsgBox("error reading/writing xml" & vbCrLf & vbCrLf & strError, MsgBoxStyle.Critical)
        Application.Exit()
    End Sub

    Public Sub CreateElement(ByVal root As String, ByVal entry As String, ByVal path As String)

        Dim cr As String = Environment.NewLine
        '  Dim newEntry As String = "<job_5 name='' done='False' local_copy='False' filename='' use_filename='False' argument='' use_argument='False' comment='' />"

        '   Dim xmlData As String = "<book xmlns:bk='urn:samples'></book>"
        If path <> strOldPath Then
            xmldoc.Load(path)
            strOldPath = path
        End If

        ' Create a new XmlDocumentFragment for our document.
        Dim docFrag As XmlDocumentFragment = xmldoc.CreateDocumentFragment()
        ' The Xml for this fragment is our newPerson string.
        docFrag.InnerXml = entry
        ' The root element of our file is found using
        ' the DocumentElement property of the XmlDocument.
        Dim xmlroot As XmlNode = xmldoc.SelectSingleNode(root)
        ' Append our new Person to the root element.
        xmlroot.AppendChild(docFrag)

        xmldoc.Save(path)

    End Sub

End Module
