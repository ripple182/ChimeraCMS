<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div class="modal fade" data-bind="showModal: moduleUploadingNewImageFor, with: moduleUploadingNewImageFor">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" aria-hidden="true" data-bind="click: $root.closeUploadModuleImage">&times;</button>
                <h4 class="modal-title">Edit Image</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-lg-3">
                        <div class="thumbnail">
                            <h5 class="text-center">Current Image</h5>
                            <img data-bind="attr: { src: $root.getModuleUploadingImageSource() }" class="img-responsive" />
                        </div>
                    </div>
                    <div class="col-lg-9">
                        <div class="well">
                            <h5>Image Gallery</h5>
                            <h6>click on an image below to replace the current image</h6>
                            <!-- ko foreach: { data: $root.imageList().ImageList, as: 'image' } -->
                                <div class="thumbnail chimera-gallery-image-thumbnail" data-bind="click: function () { $root.setModuleUploadImageSourceFromGalleryImage(image) }">
                                    <img data-bind="attr: { src: $root.getGalleryImageSource(image) }" />    
                                </div>
                            <!-- /ko -->
                        </div>
                    </div>
                </div>
                    <div class="span12">
                        &nbsp;
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <h5>Upload new images to the gallery below</h5>
                        </div>
                    </div>
                    <div class="row fileupload-buttonbar">
                        <form enctype="multipart/form-data">
                            <div class="col-lg-12">
                                <div class="well">
                                    <div class="input-group">
                                        <input type="text" class="form-control" readonly="" value="Click browse to select multiple files">
                                        <span class="input-group-btn">
                                            <span class="btn btn-primary btn-file">Browse…
                                            <input type="file" id="fileupload" name="fileupload" accept="image/*" multiple="multiple">
                                            </span>
                                        </span>
                                    </div>
                                    <div id="filelistholder"></div>
                                    <button id="btnUploadAll" class="btn btn-success pull-right" type="button">
                                        Upload All</button>
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
</div>
<div class="modal-footer">
                    <button type="button" class="btn btn-default" data-bind="click: $root.closeUploadModuleImage">Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->
