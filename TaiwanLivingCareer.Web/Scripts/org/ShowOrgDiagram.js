

function LoadOrgItem(data) {

    var firstItem = getFirstItem(data);

    if (firstItem != null) {
        var rootItem = getOrgItem(data, firstItem);

        var options = new primitives.orgdiagram.Config();
        options.rootItem = rootItem;
        options.cursorItem = rootItem;
        options.hasSelectorCheckbox = primitives.common.Enabled.False;
        options.templates = [getTLCOrgTemplate()];
        options.onItemRender = onTemplateRender;

        jQuery(".basicdiagram").orgDiagram(options);
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