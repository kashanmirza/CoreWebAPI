using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

    //    private IRoleService _roleService;

    //    public RoleController(IRoleService roleService)
    //    {
    //        _roleService = roleService;
    //    }


    //    [AllowAnonymous]
    //    [HttpGet("[action]")]
    //    public IActionResult GetAllPermisssions()
    //    {
    //        var roles = _roleService.GetAllPermissions();
    //        var parent = roles.Where(x => x.ParentPermissionId == null).Select(c => new RoleNode { value = c.Id.Value, label = c.PermissionName, children = addChild(c.Id, roles) }).ToList();
    //        //RoleNode rn = new RoleNode();
    //        //var abc = roles.Where(x => x.ParentPermissionId == null).Select(c => new RoleNode { value = c.Id.Value, label = c.PermissionName }).ToList();

    //        //foreach (var item in abc.ToList())
    //        //{
    //        //    item.children =  addChildsssss(item, roles);
    //        //}
    //        return Ok(parent);
    //    }

    //    [AllowAnonymous]
    //    [HttpGet("[action]")]
    //    public IActionResult GetAllUsers()
    //    {
    //        var roles = _roleService.GetAllUsers();
    //        Dropdown dropdown = new Dropdown();
    //        dropdown.value = "-1";
    //        dropdown.lable = "All";
    //        roles.Insert(0, dropdown);
    //        return Ok(roles);
    //    }

    //    [AllowAnonymous]
    //    [HttpPost("[action]")]
    //    public IActionResult SaveRolePermisssions(RoleData roleData)
    //    {
    //        return Content("Record Save has benn save SuccessFully");
    //    }

    //    [AllowAnonymous]
    //    [HttpPost("[action]")]
    //    public IActionResult GetPermisssionsById(IdData dataOne)
    //    {
    //        try
    //        {
    //            if (dataOne.Permission.Id != null)
    //            {
    //                // var roles = _roleService.GetAllPermissions();
    //                //roles = roles.Where(x => x.Id == int.Parse(id.ToString())).ToList();
    //                //var parent = roles.Where(x => x.ParentPermissionId == null).Select(c => new RoleNode { value = c.Id.Value, label = c.PermissionName, children = addChild(c.Id, roles) }).ToList();
    //                //var checkedNode = parent.Select(c => c.value).ToList();
    //                RoleData roleData = new RoleData();
    //                List<string> checkedNode = new List<string>();
    //                checkedNode.Add("1");
    //                checkedNode.Add("7");
    //                roleData.Permissions = checkedNode;
    //                roleData.Role = new RoleProperties();
    //                roleData.Role.Name = "Osama";
    //                roleData.Role.Description = "This is testing";
    //                roleData.Role.Comments = "helloo you are onn";

    //                return Ok(roleData);
    //            }
    //            else
    //            {
    //                return NotFound("Record Not Found");
    //            }
    //        }
    //        catch (System.Exception ex)
    //        {
    //            return NotFound("Something Went wrong" + ex.Message.ToString());
    //        }

    //    }


    //    [AllowAnonymous]
    //    [HttpPost("[action]")]
    //    public IActionResult ApproveRole(IdData dataOne /*[FromBody]JObject approvedId*/)
    //    {
    //        try
    //        {
    //            //if (approvedId["PermissionId"]["Id"] != null)
    //            //{
    //            //    string approveId = approvedId["PermissionId"]["Id"].ToString();
    //            //    // var roles = _roleService.GetAllPermissions();
    //            //    //roles = roles.Where(x => x.Id == int.Parse(id.ToString())).ToList();
    //            //    //var parent = roles.Where(x => x.ParentPermissionId == null).Select(c => new RoleNode { value = c.Id.Value, label = c.PermissionName, children = addChild(c.Id, roles) }).ToList();
    //            //    //var checkedNode = parent.Select(c => c.value).ToList();
    //            //    List<string> checkedNode = new List<string>();
    //            //    checkedNode.Add("1");
    //            //    checkedNode.Add("7");
    //            //    return Ok(checkedNode);
    //            //}
    //            if (dataOne.Permission.Id != null)
    //            {
    //                List<string> checkedNode = new List<string>();
    //                checkedNode.Add("1");
    //                checkedNode.Add("7");
    //                return Ok(checkedNode);
    //            }
    //            else
    //            {
    //                return NotFound("Record Not Found");
    //            }
    //        }
    //        catch (System.Exception ex)
    //        {
    //            return NotFound("Something Went wrong" + ex.Message.ToString());
    //        }

    //    }


    //    [AllowAnonymous]
    //    [HttpGet("[action]")]
    //    public IActionResult GetRolesFromRolePermission()
    //    {
    //        try
    //        {
    //            if (true)
    //            {
    //                // var roles = _roleService.GetAllPermissions();
    //                //roles = roles.Where(x => x.Id == int.Parse(id.ToString())).ToList();
    //                //var parent = roles.Where(x => x.ParentPermissionId == null).Select(c => new RoleNode { value = c.Id.Value, label = c.PermissionName, children = addChild(c.Id, roles) }).ToList();
    //                //var checkedNode = parent.Select(c => c.value).ToList();
    //                RoleGrid roleGrid = new RoleGrid();
    //                roleGrid.ID = "1";
    //                roleGrid.Description = "This is testing one";
    //                roleGrid.CreatedBy = "Osama";
    //                roleGrid.RoleName = "Yasir";
    //                roleGrid.Status = "Rejected";

    //                List<RoleGrid> roleGrids = new List<RoleGrid>();
    //                roleGrids.Add(roleGrid);
    //                roleGrid = new RoleGrid();
    //                roleGrid.ID = "2";
    //                roleGrid.Description = "this is two";
    //                roleGrid.CreatedBy = "Nuaman";
    //                roleGrid.RoleName = "Tariq";
    //                roleGrid.Status = "Pending";


    //                roleGrids.Add(roleGrid);

    //                roleGrid = new RoleGrid();
    //                roleGrid.ID = "3";
    //                roleGrid.Description = "this is two";
    //                roleGrid.CreatedBy = "waleed";
    //                roleGrid.RoleName = "Salman";
    //                roleGrid.Status = "Active";

    //                roleGrids.Add(roleGrid);
    //                return Ok(roleGrids);
    //            }
    //            else
    //            {
    //                return NotFound("Record Not Found");
    //            }
    //        }
    //        catch (System.Exception ex)
    //        {
    //            return NotFound("Something Went wrong" + ex.Message.ToString());
    //        }

    //    }

    //    [AllowAnonymous]
    //    [HttpPost("[action]")]
    //    public IActionResult RejectRole(IdData dataOne /*[FromBody]JObject approvedId*/)
    //    {
    //        try
    //        {
    //            //if (approvedId["PermissionId"]["Id"] != null)
    //            //{
    //            //    string approveId = approvedId["PermissionId"]["Id"].ToString();
    //            //    // var roles = _roleService.GetAllPermissions();
    //            //    //roles = roles.Where(x => x.Id == int.Parse(id.ToString())).ToList();
    //            //    //var parent = roles.Where(x => x.ParentPermissionId == null).Select(c => new RoleNode { value = c.Id.Value, label = c.PermissionName, children = addChild(c.Id, roles) }).ToList();
    //            //    //var checkedNode = parent.Select(c => c.value).ToList();
    //            //    List<string> checkedNode = new List<string>();
    //            //    checkedNode.Add("1");
    //            //    checkedNode.Add("7");
    //            //    return Ok(checkedNode);
    //            //}
    //            if (dataOne.Permission.Id != null)
    //            {
    //                List<string> checkedNode = new List<string>();
    //                checkedNode.Add("1");
    //                checkedNode.Add("7");
    //                return Ok(checkedNode);
    //            }
    //            else
    //            {
    //                return NotFound("Record Not Found");
    //            }
    //        }
    //        catch (System.Exception ex)
    //        {
    //            return NotFound("Something Went wrong" + ex.Message.ToString());
    //        }

    //    }
    //    [AllowAnonymous]
    //    [HttpPost("[action]")]
    //    public IActionResult SearchRole(SearchRole searchRole)
    //    {
    //        RoleGrid roleGrid = new RoleGrid();
    //        roleGrid.ID = "1";
    //        roleGrid.Description = "This is testing one";
    //        roleGrid.CreatedBy = "Osama";
    //        roleGrid.RoleName = "Yasir";
    //        roleGrid.Status = "Rejected";

    //        List<RoleGrid> roleGrids = new List<RoleGrid>();
    //        roleGrids.Add(roleGrid);
    //        roleGrid = new RoleGrid();
    //        roleGrid.ID = "2";
    //        roleGrid.Description = "this is two";
    //        roleGrid.CreatedBy = "Nuaman";
    //        roleGrid.RoleName = "Tariq";
    //        roleGrid.Status = "Pending";


    //        roleGrids.Add(roleGrid);

    //        roleGrid = new RoleGrid();
    //        roleGrid.ID = "3";
    //        roleGrid.Description = "this is two";
    //        roleGrid.CreatedBy = "dgn";
    //        roleGrid.RoleName = "dgn";
    //        roleGrid.Status = "Approved";

    //        roleGrids.Add(roleGrid);
    //        if (searchRole.Role.Status.ToLower() == "all")
    //        {
    //            return Ok(roleGrids);
    //        }

    //        return Ok(roleGrids.Where(x => x.Status.ToLower() == searchRole.Role.Status.ToLower()).ToList());
    //    }

    //    [AllowAnonymous]
    //    [HttpPost("[action]")]
    //    public IActionResult UpdateRolePermission(RoleData roleData)
    //    {
    //        return Content("Record Save has been updated SuccessFully");
    //    }

    //    [AllowAnonymous]
    //    [HttpPost("[action]")]
    //    public IActionResult SearchAll(SearchUserManagement searchUserManagement)
    //    {
    //        UserManagementGrid userManagementGrid = new UserManagementGrid();
    //        userManagementGrid.ID = "1";
    //        userManagementGrid.LastName = "This is testing one";
    //        userManagementGrid.CreatedBy = "Osama";
    //        userManagementGrid.FirstName = "Yasir";
    //        userManagementGrid.Status = "Rejected";
    //        userManagementGrid.Mobile = "0424544542";

    //        List<UserManagementGrid> userManagementGrids = new List<UserManagementGrid>();
    //        userManagementGrids.Add(userManagementGrid);
    //        userManagementGrid = new UserManagementGrid();
    //        userManagementGrid.ID = "2";
    //        userManagementGrid.LastName = "this is two";
    //        userManagementGrid.CreatedBy = "Nuaman";
    //        userManagementGrid.FirstName = "Tariq";
    //        userManagementGrid.Status = "Pending";
    //        userManagementGrid.Mobile = "555551111";


    //        userManagementGrids.Add(userManagementGrid);

    //        userManagementGrid = new UserManagementGrid();
    //        userManagementGrid.ID = "3";
    //        userManagementGrid.LastName = "this is two";
    //        userManagementGrid.CreatedBy = "dgn";
    //        userManagementGrid.FirstName = "dgn";
    //        userManagementGrid.Status = "Approved";
    //        userManagementGrid.Mobile = "77844452145";

    //        userManagementGrids.Add(userManagementGrid);
    //        if (searchUserManagement.User.Status.ToLower() == "all")
    //        {
    //            return Ok(userManagementGrids);
    //        }

    //        return Ok(userManagementGrids.Where(x => x.Status.ToLower() == searchUserManagement.User.Status.ToLower()).ToList());
    //    }


    //    [AllowAnonymous]
    //    [HttpGet("[action]")]
    //    public IActionResult GetActiveRole()
    //    {
    //        RoleGrid roleGrid = new RoleGrid();
    //        roleGrid.ID = "1";
    //        roleGrid.Description = "This is testing one";
    //        roleGrid.RoleName = "Yasir";

    //        List<RoleGrid> roleGrids = new List<RoleGrid>();
    //        roleGrids.Add(roleGrid);
    //        roleGrid = new RoleGrid();
    //        roleGrid.ID = "2";
    //        roleGrid.Description = "this is two";
    //        roleGrid.RoleName = "Tariq";


    //        roleGrids.Add(roleGrid);

    //        roleGrid = new RoleGrid();
    //        roleGrid.ID = "3";
    //        roleGrid.Description = "this is two";
    //        roleGrid.RoleName = "dgn";

    //        roleGrids.Add(roleGrid);

    //        var data = roleGrids.ToList().Select(c => new { ID = c.ID, RoleName = c.RoleName, Description = c.Description }).ToList();
    //        return Ok(data);

    //    }

    //    [AllowAnonymous]
    //    [HttpPost("[action]")]
    //    public IActionResult SaveUserPermission(UserManagementData userData)
    //    {
    //        return Content("Record Save has benn save SuccessFully");
    //    }

    //    [AllowAnonymous]
    //    [HttpPost("[action]")]
    //    public IActionResult GetUserManagementPermisssionsById(IdData dataOne)
    //    {
    //        try
    //        {
    //            if (dataOne.Permission.Id != null)
    //            {
    //                // var roles = _roleService.GetAllPermissions();
    //                //roles = roles.Where(x => x.Id == int.Parse(id.ToString())).ToList();
    //                //var parent = roles.Where(x => x.ParentPermissionId == null).Select(c => new RoleNode { value = c.Id.Value, label = c.PermissionName, children = addChild(c.Id, roles) }).ToList();
    //                //var checkedNode = parent.Select(c => c.value).ToList();
    //                UserManagementData userData = new UserManagementData();
    //                List<string> checkedNode = new List<string>();
    //                checkedNode.Add("1");
    //                checkedNode.Add("3");
    //                userData.Permissions = checkedNode;
    //                userData.userManagement = new UserManagementProperties();
    //                userData.userManagement.FirstName = "Osama";
    //                userData.userManagement.LastName = "This is testing";
    //                userData.userManagement.Password = "******";

    //                return Ok(userData);
    //            }
    //            else
    //            {
    //                return NotFound("Record Not Found");
    //            }
    //        }
    //        catch (System.Exception ex)
    //        {
    //            return NotFound("Something Went wrong" + ex.Message.ToString());
    //        }

    //    }

    //    [AllowAnonymous]
    //    [HttpPost("[action]")]
    //    public IActionResult UpdateUserPermission(UserManagementData roleData)
    //    {
    //        return Content("Record Save has been updated SuccessFully");
    //    }

    //    [AllowAnonymous]
    //    [HttpPost("[action]")]
    //    public IActionResult ApproveUserManagement(IdData dataOne /*[FromBody]JObject approvedId*/)
    //    {
    //        try
    //        {
    //            //if (approvedId["PermissionId"]["Id"] != null)
    //            //{
    //            //    string approveId = approvedId["PermissionId"]["Id"].ToString();
    //            //    // var roles = _roleService.GetAllPermissions();
    //            //    //roles = roles.Where(x => x.Id == int.Parse(id.ToString())).ToList();
    //            //    //var parent = roles.Where(x => x.ParentPermissionId == null).Select(c => new RoleNode { value = c.Id.Value, label = c.PermissionName, children = addChild(c.Id, roles) }).ToList();
    //            //    //var checkedNode = parent.Select(c => c.value).ToList();
    //            //    List<string> checkedNode = new List<string>();
    //            //    checkedNode.Add("1");
    //            //    checkedNode.Add("7");
    //            //    return Ok(checkedNode);
    //            //}
    //            if (dataOne.Permission.Id != null)
    //            {
    //                List<string> checkedNode = new List<string>();
    //                checkedNode.Add("1");
    //                checkedNode.Add("7");
    //                return Ok(checkedNode);
    //            }
    //            else
    //            {
    //                return NotFound("Record Not Found");
    //            }
    //        }
    //        catch (System.Exception ex)
    //        {
    //            return NotFound("Something Went wrong" + ex.Message.ToString());
    //        }

    //    }

    //    [AllowAnonymous]
    //    [HttpPost("[action]")]
    //    public IActionResult RejectUserManagement(IdData dataOne /*[FromBody]JObject approvedId*/)
    //    {
    //        try
    //        {
    //            //if (approvedId["PermissionId"]["Id"] != null)
    //            //{
    //            //    string approveId = approvedId["PermissionId"]["Id"].ToString();
    //            //    // var roles = _roleService.GetAllPermissions();
    //            //    //roles = roles.Where(x => x.Id == int.Parse(id.ToString())).ToList();
    //            //    //var parent = roles.Where(x => x.ParentPermissionId == null).Select(c => new RoleNode { value = c.Id.Value, label = c.PermissionName, children = addChild(c.Id, roles) }).ToList();
    //            //    //var checkedNode = parent.Select(c => c.value).ToList();
    //            //    List<string> checkedNode = new List<string>();
    //            //    checkedNode.Add("1");
    //            //    checkedNode.Add("7");
    //            //    return Ok(checkedNode);
    //            //}
    //            if (dataOne.Permission.Id != null)
    //            {
    //                List<string> checkedNode = new List<string>();
    //                checkedNode.Add("1");
    //                checkedNode.Add("7");
    //                return Ok(checkedNode);
    //            }
    //            else
    //            {
    //                return NotFound("Record Not Found");
    //            }
    //        }
    //        catch (System.Exception ex)
    //        {
    //            return NotFound("Something Went wrong" + ex.Message.ToString());
    //        }

    //    }
    //    private List<RoleNode> addChild(int? id, List<Permission> allpermissions)
    //    {
    //        RoleNode rn = new RoleNode();
    //        var listOfNodes = allpermissions.Where(x => x.ParentPermissionId == id).Select(c => new RoleNode { value = c.Id.Value, label = c.PermissionName, children = addChild(c.Id, allpermissions) }).ToList();
    //        if (listOfNodes == null)
    //        {
    //            return null;
    //        }

    //        return listOfNodes;
    //    }


    //    //private List<RoleNode> addChildsssss(RoleNode rn, List<Permission> allpermissions)
    //    //{
    //    //    List<RoleNode> rlist = new List<RoleNode>();
    //    //         RoleNode rnChild = new RoleNode();
    //    //    var listOfNodes = allpermissions.Where(x => x.ParentPermissionId == rn.value).Select(c => new RoleNode { value = c.Id.Value, label = c.PermissionName }).ToList();
    //    //    if (listOfNodes.Count == 0)
    //    //    {
    //    //        return null;
    //    //    }
    //    //    else
    //    //    {
    //    //        for (int i =0; i < listOfNodes.Count;i++)
    //    //        {
    //    //          listOfNodes[i].children = addChildsssss(listOfNodes[i], allpermissions);
    //    //        }
    //    //        return listOfNodes;
    //    //    }


    //    //}
    }


}