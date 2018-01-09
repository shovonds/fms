(function () {
  
    //alert("hello");
    
    var app = angular.module('MyApp', ['angularTreeview', 'angularBootstrapNavTree']);
    app.controller('appControl', ['$scope', '$http', function ($scope, $http) {
       // GetTreeDir();
      //  Getfile(20180106204810);
        //  GetDirList();
        $scope.title = 'My Content';
        $scope.submit = 'SAVE';
        $scope.newFolder = {};

        $scope.fileList = [];
        $scope.curFile;
        $scope.ImageProperty = {
            file: ''
        }

        $scope.getTreeDir = function () {

            $http.get('/Directory/GetDirStructure').success(function (data) {
                $scope.TreeList = data.treeDirectory;
               // console.log($scope.TreeList);

            });
        }

        function Getfile(dirId) {
            $http.get('/File/fileList?dir_id=' + dirId).
                success(function (data) {
                    $scope.newFileList = data;
                })
        }

        $scope.fileGetList = function (dirId) {

            $http.get('/File/fileList?dir_id=' + dirId).
                success(function (data) {
                    $scope.newFileList = data;
                })
        };

        $scope.saveFolder = function (p_id) {
            debugger
            $scope.submit = 'Saving..';
            $scope.newFolder.parent_id = p_id;
           // $scope.taskNew.TaskDate = new Date();
            $http({
                method: 'POST',
                url: '/Directory/AddNewDir',
                data: $scope.newFolder
            }).success(function () {
                $scope.newFolder = null;
                $scope.submit = 'SAVE';
                $scope.getTreeDir();
               // GetDirList();
            }).error(function () {
                alert(data.errors);
            })
        };


        $scope.setFile = function (element) {
            $scope.fileList = [];
            // get the files
            var files = element.files;
            for (var i = 0; i < files.length; i++) {
                $scope.ImageProperty.file = files[i];

                $scope.fileList.push($scope.ImageProperty);
                $scope.ImageProperty = {};
                $scope.$apply();

            }
        }

        $scope.UploadFile = function (dir_id) {

            for (var i = 0; i < $scope.fileList.length; i++) {

                $scope.UploadFileIndividual($scope.fileList[i].file,
                                            $scope.fileList[i].file.name,
                                            $scope.fileList[i].file.type,
                                            $scope.fileList[i].file.size,
                                            dir_id,
                                            i);
            }

            $scope.fileGetList(dir_id);



        }

        $scope.UploadFileIndividual = function (fileToUpload, name, type, size, dir_id, index) {
            //Create XMLHttpRequest Object
            var reqObj = new XMLHttpRequest();

            reqObj.upload.addEventListener("progress", uploadProgress, false)
            reqObj.addEventListener("load", uploadComplete, false)
            reqObj.addEventListener("error", uploadFailed, false)
            reqObj.addEventListener("abort", uploadCanceled, false)


            //open the object and set method of call(get/post), url to call, isAsynchronous(true/False)
            reqObj.open("POST", "/File/UploadFiles", true);

            //set Content-Type at request header.for file upload it's value must be multipart/form-data
            reqObj.setRequestHeader("Content-Type", "multipart/form-data");

            //Set Other header like file name,size and type
            reqObj.setRequestHeader('X-File-Name', name);
            reqObj.setRequestHeader('X-File-Type', type);
            reqObj.setRequestHeader('X-File-Size', size);
            reqObj.setRequestHeader('X-File-dir', dir_id);

            // send the file
            reqObj.send(fileToUpload);

            function uploadProgress(evt) {
                if (evt.lengthComputable) {

                    var uploadProgressCount = Math.round(evt.loaded * 100 / evt.total);

                    document.getElementById('P' + index).innerHTML = uploadProgressCount;

                    if (uploadProgressCount == 100) {
                        document.getElementById('P' + index).innerHTML =
                       '<i class="fa fa-refresh fa-spin" style="color:maroon;"></i>';
                    }

                }
            }

            function uploadComplete(evt) {
                /* This event is raised when the server  back a response */

                document.getElementById('P' + index).innerHTML = 'Saved';
                $scope.NoOfFileSaved++;
                $scope.$apply();
            }

            function uploadFailed(evt) {
                document.getElementById('P' + index).innerHTML = 'Upload Failed..';
            }

            function uploadCanceled(evt) {

                document.getElementById('P' + index).innerHTML = 'Canceled....';
            }


        }


        $scope.addUser = function () {
            $scope.userStat = 'Saving... Please wait';
            $http({
                method: 'POST',
                url: '/Home/addNewUser',
                data: $scope.newUser
            }).success(function (response) {
                $scope.userStat = response;
                $scope.newUser = null;

            })
        }

        $scope.allMenuList = function () {
            $http.get('/Role/allMenuList').
                success(function (data) {
                    $scope.menuListAll = data;
                    console.log(data);

                })
        }


        $scope.SaveRole = function () {
            $scope.menusave = 'Saving..'
            $http({
                method: 'POST',
                url: '/Role/AddRole',
                data:$scope.roleData
            }).success(function (response) {
                if (response!=null)
                {
                    $scope.selectedMenu(response);
                }
                
            })
        }
        
        $scope.selectedMenu = function (role_id) {
            $scope.selecteditem = [];
            for (var i = 0; i < $scope.menuListAll.length; i++) {
                if ($scope.menuListAll[i].Selected) {
                    var menuid = $scope.menuListAll[i].menu_id;
                    $scope.selecteditem = menuid;
                    console.log($scope.selecteditem);
                    $scope.saveRoleAccess(role_id,$scope.selecteditem);
                }
            }
          //  $scope.saveRoleAccess(menuid);
          //  console.log(item);
        }

     
        $scope.saveRoleAccess = function (role_id,menu) {
            //$scope.menu_id = {};
            //$scope.menu_id = menuid;
            
            $scope.selecteditem_role = {};
            $scope.selecteditem_role.role_id = role_id;
            $scope.selecteditem_role.menu_id = menu;
            console.log($scope.selecteditem_role);
            $scope.menusave = 'Saving..';
            $http({
                method: 'POST',
                url: '/Role/SaveRoleMenu',
                data: $scope.selecteditem_role
            }).success(function (response) {
                $scope.menusave = 'SAVE'

            })

        }

    }]);

})();
