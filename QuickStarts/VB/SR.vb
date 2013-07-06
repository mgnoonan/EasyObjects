Imports System.Globalization
Imports System.Resources

Namespace EasyObjectsQuickStart

    Friend Class SR

        Public Shared Property Culture() As CultureInfo

            Get
                Return Keys.Culture
            End Get

            Set(ByVal Value As CultureInfo)
                Keys.Culture = Value
            End Set
        End Property

        Public Shared ReadOnly Property SimpleMessage() As String
            Get
                Return Keys.GetString(Keys.SimpleMessage)
            End Get
        End Property

        Public Shared Function FileOpenError(ByVal filename As String, ByVal description As String) As String
            Return Keys.GetString(Keys.FileOpenError, filename, description)
        End Function

        Public Shared Function ProgressMessage(ByVal iterationCount As Integer) As String
            Return Keys.GetString(Keys.ProgressMessage, iterationCount)
        End Function

        Public Shared Function GeneralExceptionMessage(ByVal message As String) As String
            Return Keys.GetString(Keys.GeneralExceptionMessage, message)
        End Function

        Public Shared ReadOnly Property DbRequirementsMessage() As String
            Get
                Return Keys.GetString(Keys.DbRequirementsMessage)
            End Get
        End Property

        Public Shared ReadOnly Property ApplicationErrorMessage() As String
            Get
                Return Keys.GetString(Keys.ApplicationErrorMessage)
            End Get
        End Property

        Public Shared ReadOnly Property AffectedRowsMessage(ByVal rows As Integer) As String
            Get
                Return Keys.GetString(Keys.AffectedRowsMessage, rows)
            End Get
        End Property

        Public Shared ReadOnly Property TransferCompletedMessage() As String
            Get
                Return Keys.GetString(Keys.TransferCompletedMessage)
            End Get
        End Property

        Public Shared ReadOnly Property TransferFailedMessage() As String
            Get
                Return Keys.GetString(Keys.TransferFailedMessage)
            End Get
        End Property

        Friend Class Keys

            Private Shared resManager As ResourceManager = _
            New ResourceManager("EasyObjectsQuickStart.SR", GetType(SR).Module.Assembly)

            Private Shared _Culture As CultureInfo = Nothing

            Public Shared Property Culture() As CultureInfo
                Get
                    Return _Culture
                End Get
                Set(ByVal Value As CultureInfo)
                    _Culture = Value
                End Set
            End Property

            Public Shared Function GetString(ByVal key As String) As String
                Return resManager.GetString(key, Culture)
            End Function

            Public Shared Function GetString(ByVal key As String, ByVal ParamArray args As Object()) As String
                Dim msg As String = resManager.GetString(key, Culture)
                msg = String.Format(msg, args)
                Return msg
            End Function

            Public Const SimpleMessage As String = "SimpleMessage"
            Public Const FileOpenError As String = "FileOpenError"
            Public Const ProgressMessage As String = "ProgressMessage"
            Public Const GeneralExceptionMessage As String = "GeneralExceptionMessage"
            Public Const DbRequirementsMessage As String = "DbRequirementsMessage"
            Public Const ApplicationErrorMessage As String = "ApplicationErrorMessage"
            Public Const AffectedRowsMessage As String = "AffectedRowsMessage"
            Public Const TransferCompletedMessage As String = "TransferCompletedMessage"
            Public Const TransferFailedMessage As String = "TransferFailedMessage"
        End Class

    End Class
End Namespace