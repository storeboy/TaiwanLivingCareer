
var m_timer = null;
var orgDiagram = null;

jQuery(document).ready(function () {
   
    jQuery.ajaxSetup({
        cache: false
    });
   
    $(window).resize(function () {
        onWindowResize();
    });

    Setup(jQuery("#centerpanel"));

    $.getJSON("/Franchisee/SettleOrgSource", "", function (data) {
        LoadOrgItem(data);
    });

    ResizePlaceholder();
});

function Setup(selector) {
    orgDiagram = selector.orgDiagram(GetOrgDiagramConfig());
}


function LoadOrgItem(data) {

    var firstItem = getFirstItem(data);

    if (firstItem != null) {
        var rootItem = getOrgItem(data, firstItem);

        var options = new primitives.orgdiagram.Config();
        options.rootItem = rootItem;
        options.cursorItem = rootItem;

        jQuery("#centerpanel").orgDiagram("option", {
            rootItem: rootItem,
            cursorItem: rootItem
        });
        jQuery("#centerpanel").orgDiagram("update");
    }
}

function getFirstItem(data) {
    var nowItems = getObjects(data, "ParentID", "");
    if (nowItems != null && nowItems.length > 0)
        return nowItems[0];
}

function getOrgItem(data, nowItem) {
    var parentID = nowItem.ID;

    var result = new primitives.orgdiagram.ItemConfig();
    result.AccountName = nowItem.Name;
    result.RecommendName = "推：" + nowItem.RecommendName;
    if (nowItem.IsVip)
        result.image = "../Content/images/vip-card.jpg?2";
    result.Orders = "單：" + nowItem.Orders;
    result.OrgLevel = "【" + nowItem.OrgLevel + "】";
    if (nowItem.OrgLevel == 1)
        result.itemTitleColor = "Red";
    else
        result.itemTitleColor = "Blue";

    result.templateName = "TLCOrgTemplate";

    var nextItems = getObjects(data, "ParentID", parentID);
    for (var i = 0; i < nextItems.length; i++) {
        var xitem = nextItems[i];
        result.items.push(getOrgItem(data, xitem));
    }

    return result;
}

function getObjects(obj, key, val) {
    var objects = [];
    for (var i in obj) {
        if (!obj.hasOwnProperty(i)) continue;
        if (typeof obj[i] == 'object') {
            objects = objects.concat(getObjects(obj[i], key, val));
        } else if (i == key && obj[key] == val) {
            objects.push(obj);
        }
    }
    return objects;
}

function GetOrgDiagramConfig() {
   

    var templates = [];
    templates.push(getTLCOrgTemplate());

    return {
        graphicsType: primitives.common.GraphicsType.Canvas,
        pageFitMode: primitives.orgdiagram.PageFitMode.FitToPage,
        orientationType: primitives.orgdiagram.OrientationType.Top,
        verticalAlignment: primitives.common.VerticalAlignmentType.Middle,
        horizontalAlignment: primitives.common.HorizontalAlignmentType.Center,
        connectorType:  primitives.orgdiagram.ConnectorType.Curved,
        minimalVisibility: primitives.orgdiagram.Visibility.Dot,
        hasSelectorCheckbox: primitives.common.Enabled.False,
        selectionPathMode: primitives.orgdiagram.SelectionPathMode.FullStack,
        leavesPlacementType: primitives.orgdiagram.ChildrenPlacementType.Horizontal,
        hasButtons:  primitives.common.Enabled.False,
        //buttons: buttons,
        templates: templates,
        //onButtonClick: onButtonClick,
        //onCursorChanging: onCursorChanging,
        //onCursorChanged: onCursorChanged,
        //onHighlightChanging: onHighlightChanging,
        //onHighlightChanged: onHighlightChanged,
        //onSelectionChanged: onSelectionChanged,
        onItemRender: onTemplateRender,
        //itemTitleFirstFontColor: primitives.common.Colors.White,
        //itemTitleSecondFontColor: primitives.common.Colors.White,
        showLabels: primitives.common.Enabled.Auto,
        labelOrientation: primitives.text.TextOrientationType.Horizontal,
        labelPlacement: primitives.common.PlacementType.Top
    };
}


function onTemplateRender(event, data) {
    switch (data.renderingMode) {
        case primitives.common.RenderingMode.Create:
            /* Initialize widgets here */
            break;
        case primitives.common.RenderingMode.Update:
            /* Update widgets here */
            break;
    }

    var itemConfig = data.context;

    if (data.templateName == "TLCOrgTemplate") {
        data.element.find("[name=VipImg]").attr({ "src": itemConfig.image, "alt": "VIP" });
        data.element.find("[name=titleBackground]").css({ "background": itemConfig.itemTitleColor });

        var fields = ["AccountName", "RecommendName", "Orders", "OrgLevel"];
        for (var index = 0; index < fields.length; index++) {
            var field = fields[index];
            var element = data.element.find("[name=" + field + "]");
            if (element.text() != itemConfig[field]) {
                element.text(itemConfig[field]);
            }
        }
    }
}

function getTLCOrgTemplate() {
    var result = new primitives.orgdiagram.TemplateConfig();
    result.name = "TLCOrgTemplate";

    result.itemSize = new primitives.common.Size(160, 60);
    result.minimizedItemSize = new primitives.common.Size(3, 3);
    result.highlightPadding = new primitives.common.Thickness(2, 2, 2, 2);
    
    var itemTemplate = jQuery(
      '<div class="bp-item bp-corner-all bt-item-frame">'
        + '<div name="titleBackground" class="bp-item bp-corner-all bp-title-frame" style="top: 2px; left: 2px; width: 216px; height: 20px;">'
            + '<div name="AccountName" class="bp-item bp-title" style="top: 3px; left: 6px; width: 80px; height: 18px;"></div>'
            + '<div name="OrgLevel" class="bp-item bp-title" style="top: 3px; left: 81px; width: 100px; height: 18px;"></div>'
        + '</div>'
        + '<div class="bp-item bp-photo-frame" style="top: 26px; left: 2px; width: 40px; height: 30px;">'
            + '<img name="VipImg" style="height=60px; width=50px;" />'
        + '</div>'
        + '<div name="RecommendName" class="bp-item" style="top: 26px; left: 56px; width: 162px; height: 18px; font-size: 12px;"></div>'
        + '<div name="Orders" class="bp-item" style="top: 44px; left: 56px; width: 162px; height: 18px; font-size: 12px;"></div>'
    + '</div>'
    ).css({
        width: result.itemSize.width + "px",
        height: result.itemSize.height + "px"
    }).addClass("bp-item bp-corner-all bt-item-frame");
    result.itemTemplate = itemTemplate.wrap('<div>').parent().html();

    return result;
}

function onWindowResize() {
    if (m_timer == null) {
        m_timer = window.setTimeout(function () {
            ResizePlaceholder();
            jQuery("#centerpanel").orgDiagram("option", GetOrgDiagramConfig());
            jQuery("#centerpanel").orgDiagram("update", primitives.orgdiagram.UpdateMode.Refresh)
            window.clearTimeout(m_timer);
            m_timer = null;
        }, 300);
    }
}


function ResizePlaceholder() {
    var bodyWidth = $(window).width() - 40
    var bodyHeight = $(window).height() - 20
    jQuery("#centerpanel").css(
    {
        "width": bodyWidth + "px",
        "height": bodyHeight + "px"
    });
}