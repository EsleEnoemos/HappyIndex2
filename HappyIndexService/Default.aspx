<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="Default.aspx.cs" Inherits="HappyIndexService.Default" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="HappyIndex2.Common" %>
<%@ Import Namespace="HappyIndexService.Data" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<title>Happy Index 2</title>
		<style type="text/css">
			body .section {
				background-color: #fff;
				border: 1px solid #e2e2e2;
				margin-top: 10px;
				padding: 10px;				
				border-radius: 4px;
			}
			#savemessage {
				display: block;
				border: solid 1px #6D8CA8;
				background-color: #FFEEC2;
				padding: 0.3em;
				margin-top: 0.3em;
				margin-bottom: 0.3em;
				margin-right: 30px;
			}
			.errorMessage {
				display: block;
				text-align: left;
				border: solid #BB7472 1px;
				border-radius: 2px;
				white-space: nowrap;
				padding: 5px;
				margin: 10px 0;
				background-color: #D8433F;
				color: #fff;
				font-size: 110%;
			}
		</style>
		<script type="text/javascript" src="/jquery-1.11.1.min.js"></script>
	</head>
	<body>
		<h1>Hej <%=User.Name %></h1>
		<asp:PlaceHolder runat="server" ID="plhSaveMessage" />
		<div class="section">
			<div class="section">
				<h3>Din vecka</h3>
				<img alt="" src="/Graphics/User/Week/500x100/<%=Week %>.png"/>
			</div>
			<% foreach( Team t in User.Teams ) { %>
			<div class="section">
				<h3>Team <%=t.Name %> vecka <%=Week %> <i>(<%=FirstDateOfWeek.Format() %> - <%=FirstDateOfWeek.AddDays( 6 ).Format() %>)</i></h3>
				<img alt="" src="/Graphics/Team/<%=t.ID %>/Week/500x100/<%=Week %>.png"/>
			</div>
			<% } %>
		</div>
		<div class="section">
			<% using( Html.BeginForm() ) { %>
			<h2>Team</h2>
			<table>
				<thead>
					<tr>
						<th>Team</th>
						<th>Du är medlem</th>
					</tr>
				</thead>
				<% foreach( Team t in DataFactory.Teams ) { %>
				<tr>
					<td><%= t.Name %></td>
					<td><input type="checkbox" name="team" value="<%= t.ID %>"<%= " checked=\"checked\"".IfTrue( User.Teams.Contains( t ) ) %>/></td>
				</tr>
				<% } %>
			</table>
			<input type="submit" value="Spara" name="updateuserteams"/>
			<% } %>
		</div>
		<div class="section">
			<h2>Lägg till team</h2>
			<% using( Html.BeginForm() ) { %>
			Namn <input type="text" name="name"/>
			<input type="submit" name="createteam" value="Spara"/>
			<% } %>
		</div>
	</body>
</html>
