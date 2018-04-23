Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes
Imports System.ComponentModel
Imports System.Collections
Imports System.Xml.Serialization
Imports System.IO
Imports DevExpress.Xpf.NavBar
Imports System.Windows.Media.Imaging

Namespace NavBarDataBinding
	Partial Public Class MainPage
		Inherits UserControl
		Public Sub New()
			InitializeComponent()
		End Sub

		Private navigationPaneView As NavigationPaneView

		Private Sub UserControl_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			navigationPaneView = New NavigationPaneView()
			navBar.View = navigationPaneView

			AddHandler navigationPaneView.ItemAdding, AddressOf OnItemAdding
			AddHandler navigationPaneView.GroupAdding, AddressOf OnGroupAdding
			AddHandler navigationPaneView.ItemSelected, AddressOf OnItemSelected

			navBar.GroupDescription = "Country"
			navBar.ItemsSource = EmployeesData.DataSource


			Dim style As Style = CType(Resources("EmployeeItemVisualStyle"), Style)
			navigationPaneView.ItemVisualStyle = style
		End Sub

		Private Sub OnGroupAdding(ByVal sender As Object, ByVal e As GroupAddingEventArgs)
			If e.Group Is Nothing OrElse e.Group.DataContext Is Nothing Then
				Return
			End If
			e.Group.Header = e.Group.DataContext.ToString()
		End Sub

		Private Sub OnItemAdding(ByVal sender As Object, ByVal e As ItemAddingEventArgs)
			If e.Item Is Nothing OrElse e.Item.DataContext Is Nothing Then
				Return
			End If
			Dim image As New Image()
			Dim bitmapImage As New BitmapImage()
			Dim stream As New MemoryStream((CType(e.Item.DataContext, Employee)).Photo)
			bitmapImage.SetSource(stream)
			e.Item.ImageSource = bitmapImage

			e.Item.Content = (CType(e.Item.DataContext, Employee)).FirstName
		End Sub

		Private Sub OnItemSelected(ByVal sender As Object, ByVal e As NavBarItemSelectedEventArgs)
			'...
		End Sub
	End Class


	<XmlRoot("Employees")> _
	Public Class EmployeesData
		Inherits List(Of Employee)
		Public Shared Function GetDataStreamFromResources(ByVal filename As String) As Stream
			Return GetType(EmployeesData).Assembly.GetManifestResourceStream("" & filename)
		End Function
		Public Shared ReadOnly Property DataSource() As IList
			Get
				Dim s As New XmlSerializer(GetType(EmployeesData))
				Return CType(s.Deserialize(GetDataStreamFromResources("Employees.xml")), IList)
			End Get
		End Property
	End Class


	Public Class Employee
		Private employeeID_Renamed As Integer
		Public Property EmployeeID() As Integer
			Get
				Return employeeID_Renamed
			End Get
			Set(ByVal value As Integer)
				If employeeID_Renamed = value Then
					Return
				End If
				employeeID_Renamed = value
			End Set
		End Property
		Private privateLastName As String
		Public Property LastName() As String
			Get
				Return privateLastName
			End Get
			Set(ByVal value As String)
				privateLastName = value
			End Set
		End Property
		Private privateFirstName As String
		Public Property FirstName() As String
			Get
				Return privateFirstName
			End Get
			Set(ByVal value As String)
				privateFirstName = value
			End Set
		End Property
		Private privateTitle As String
		Public Property Title() As String
			Get
				Return privateTitle
			End Get
			Set(ByVal value As String)
				privateTitle = value
			End Set
		End Property
		Private privateTitleOfCourtesy As String
		Public Property TitleOfCourtesy() As String
			Get
				Return privateTitleOfCourtesy
			End Get
			Set(ByVal value As String)
				privateTitleOfCourtesy = value
			End Set
		End Property
		Private privateBirthDate As DateTime
		Public Property BirthDate() As DateTime
			Get
				Return privateBirthDate
			End Get
			Set(ByVal value As DateTime)
				privateBirthDate = value
			End Set
		End Property
		Private privateHireDate As DateTime
		Public Property HireDate() As DateTime
			Get
				Return privateHireDate
			End Get
			Set(ByVal value As DateTime)
				privateHireDate = value
			End Set
		End Property
		Private privateAddress As String
		Public Property Address() As String
			Get
				Return privateAddress
			End Get
			Set(ByVal value As String)
				privateAddress = value
			End Set
		End Property
		Private privateCity As String
		Public Property City() As String
			Get
				Return privateCity
			End Get
			Set(ByVal value As String)
				privateCity = value
			End Set
		End Property
		Private privateRegion As String
		Public Property Region() As String
			Get
				Return privateRegion
			End Get
			Set(ByVal value As String)
				privateRegion = value
			End Set
		End Property
		Private privatePostalCode As String
		Public Property PostalCode() As String
			Get
				Return privatePostalCode
			End Get
			Set(ByVal value As String)
				privatePostalCode = value
			End Set
		End Property
		Private privateCountry As String
		Public Property Country() As String
			Get
				Return privateCountry
			End Get
			Set(ByVal value As String)
				privateCountry = value
			End Set
		End Property
		Private privateHomePhone As String
		Public Property HomePhone() As String
			Get
				Return privateHomePhone
			End Get
			Set(ByVal value As String)
				privateHomePhone = value
			End Set
		End Property
		Private privateExtension As String
		Public Property Extension() As String
			Get
				Return privateExtension
			End Get
			Set(ByVal value As String)
				privateExtension = value
			End Set
		End Property
		Private privateSalary As Double
		Public Property Salary() As Double
			Get
				Return privateSalary
			End Get
			Set(ByVal value As Double)
				privateSalary = value
			End Set
		End Property
		Private privateOnVacation As Boolean
		Public Property OnVacation() As Boolean
			Get
				Return privateOnVacation
			End Get
			Set(ByVal value As Boolean)
				privateOnVacation = value
			End Set
		End Property
		Private privatePhoto As Byte()
		Public Property Photo() As Byte()
			Get
				Return privatePhoto
			End Get
			Set(ByVal value As Byte())
				privatePhoto = value
			End Set
		End Property
		Private privateNotes As String
		Public Property Notes() As String
			Get
				Return privateNotes
			End Get
			Set(ByVal value As String)
				privateNotes = value
			End Set
		End Property
		Private privateReportsTo As Integer
		Public Property ReportsTo() As Integer
			Get
				Return privateReportsTo
			End Get
			Set(ByVal value As Integer)
				privateReportsTo = value
			End Set
		End Property
	End Class
End Namespace
