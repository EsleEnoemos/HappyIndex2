<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="Default.aspx.cs" Inherits="HappyIndexService.Default" %>
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
			}
		</style>
	</head>
	<body>
		<form id="form1" runat="server">
			<div class="section">
				Hej <%=User.Name %>
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
		</form>
	</body>
</html>
