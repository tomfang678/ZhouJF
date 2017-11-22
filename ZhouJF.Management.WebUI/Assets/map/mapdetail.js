$(document).ready(function() {
	var k = new BMap.Map("mapDiv");
	k.enableScrollWheelZoom();
	var j = "";
	var l = new BMap.Geocoder();
	var c = $("#ctl00_ContentPlaceHolder1_CommunityName");
	var b = c.html();
	var a = "重庆市" + c.html();
	var i = $("#lnt");
	var g = $("#lat");
	var h;
	var f;
	if (i.val() && g.val()) {
		h = i.val();
		f = g.val()
	}
	if (h != "" && f != "") {
		q(h, f)
	} else {
		l.getPoint(a, function(r) {
			if (r) {
				h = r.lng;
				f = r.lat;
				q(h, f)
			}
		})
	}
	var m;

	function q(s, r) {
		m = new BMap.Point(s, r);
		if (s == "" || r == "") {
		    k.centerAndZoom(new BMap.Point(106.540000, 29.570000), 13)
		} else {
			k.centerAndZoom(m, 16);
			var t = new BMap.Marker(m);
			k.addOverlay(t);
			t.setAnimation(BMAP_ANIMATION_BOUNCE)
		}
		k.addControl(new BMap.NavigationControl())
	}
	$("#busline_querybutton").bind("click", n);
	$(".gongjiaoline").keydown(function(r) {
		if (r.keyCode == 13) {
			$("#busline_querybutton").trigger("click")
		}
	});

	function n() {
		$("#bytaxi_price").html("");
		k.clearOverlays();
		var t = new BMap.Point(h, f);
		var s = $("#gongjiaoline_text").val();
		if (s == "" || s == "如：您的工作单位" || s.length < 1) {
			$("#gongjiaoline_text").focus();
			$("#gjTips").addClass("in").show();
			return
		} else {
			$("#gjTips").removeClass(".in").hide()
		}
		var u = new BMap.TransitRoute(k, {
			renderOptions: {
				map: k
			}
		});
		u.setSearchCompleteCallback(function(z) {
			if (u.getStatus() == BMAP_STATUS_SUCCESS) {
				$("#results").html("");
				k.clearOverlays();
				$("<div class='map_starticon'>起点:此房源所在位置</div>").appendTo($(".gongjiaoreasult"));
				for (var x = 0; x < z.getNumPlans(); x++) {
					var A = document.createElement("div");
					var w = d(k, z, x, A);
					A.style.lineHeight = "20px";
					A.onclick = w;
					A.className = "map_listitem";
					A.innerHTML = '<span class="name">推荐线路' + (x + 1) + '</span><span class="time">（' + z.getPlan(x).getDistance() + z.getPlan(x).getDuration() + "）</span><p>" + z.getPlan(x).getDescription() + "</p>";
					document.getElementById("results").appendChild(A);
					if (x > 1) {
						break
					}
				}
				$("<div class='map_starticon'>终点:" + z.getEnd().title + "</div>").appendTo($(".gongjiaoreasult"))
			} else {
				k.clearOverlays();
				k.centerAndZoom(m, 16);
				var y = new BMap.Marker(m);
				k.addOverlay(y);
				y.setAnimation(BMAP_ANIMATION_BOUNCE);
				$("#gjTips").show();
				return
			}
		});
		u.search(t, s);

		function v(w) {
			if (r.getStatus() == BMAP_STATUS_SUCCESS) {
				$("#bytaxi_price").html(w.taxiFare.day.totalFare + "元")
			}
		}
		var r = new BMap.DrivingRoute(k, {
			onSearchComplete: v,
			renderOptions: {
				map: k,
				autoViewport: true
			}
		});
		r.search(t, s)
	}
	var e = null;

	function d(r, u, s, t) {
		return function() {
			var A = 0.45;
			var B = u.getPlan(s);
			var x = new Array();
			var v = function(L, G, H, M) {
				var N;
				var O;
				var F;
				var K;
				if (G == 1) {
					N = "http://map.baidu.com/image/dest_markers.png";
					O = 42;
					F = 34;
					K = new BMap.Icon(N, new BMap.Size(O, F), {
						offset: new BMap.Size(14, 32),
						imageOffset: new BMap.Size(0, 0 - H * F)
					})
				} else {
					N = "http://map.baidu.com/image/trans_icons.png";
					O = 22;
					F = 25;
					var E = 25;
					var D = 0;
					var I = 0;
					if (H == 2) {
						E = 21;
						D = 5;
						I = 1
					}
					K = new BMap.Icon(N, new BMap.Size(O, E), {
						offset: new BMap.Size(10, (11 + I)),
						imageOffset: new BMap.Size(0, 0 - H * F - D)
					})
				}
				var J = new BMap.Marker(L, {
					icon: K
				});
				if (M != null && M != "") {
					J.setTitle(M)
				}
				if (G == 1) {
					J.setTop(true)
				}
				r.addOverlay(J)
			};
			var w = function(E) {
				for (var D = 0; D < E.length; D++) {
					x.push(E[D])
				}
			};
			r.clearOverlays();
			for (var y = 0; y < B.getNumRoutes(); y++) {
				var C = B.getRoute(y);
				if (C.getDistance(false) > 0) {
					r.addOverlay(new BMap.Polyline(C.getPath(), {
						strokeStyle: "dashed",
						strokeColor: "#30a208",
						strokeOpacity: 0.75,
						strokeWeight: 4,
						enableMassClear: true
					}))
				}
			}
			for (y = 0; y < B.getNumLines(); y++) {
				var z = B.getLine(y);
				w(z.getPath());
				if (z.type == BMAP_LINE_TYPE_BUS) {
					v(z.getGetOnStop().point, 2, 2, z.getGetOnStop().title);
					v(z.getGetOffStop().point, 2, 2, z.getGetOffStop().title)
				} else {
					if (z.type == BMAP_LINE_TYPE_SUBWAY) {
						v(z.getGetOnStop().point, 2, 3, z.getGetOnStop().title);
						v(z.getGetOffStop().point, 2, 3, z.getGetOffStop().title)
					}
				}
				r.addOverlay(new BMap.Polyline(z.getPath(), {
					strokeColor: "#0030ff",
					strokeOpacity: A,
					strokeWeight: 6,
					enableMassClear: true
				}))
			}
			r.setViewport(x);
			v(u.getEnd().point, 1, 1);
			v(u.getStart().point, 1, 0);
			if (e != null) {
				e.style.backgroundColor = "#fff"
			}
			t.style.backgroundColor = "#f0f0f0";
			e = t
		}
	}
	document.getElementById("dvPolicy").onclick = function(s) {
		var w = new BMap.Point(h, f);
		var u = $.trim($("#gongjiaoline_text").val());
		if (u == "" || u == "如:您的工作地址") {
			$("#gongjiaoline_text").focus();
			return
		}
		var x = [BMAP_TRANSIT_POLICY_LEAST_TIME, BMAP_TRANSIT_POLICY_AVOID_SUBWAYS, BMAP_TRANSIT_POLICY_LEAST_TRANSFER, BMAP_TRANSIT_POLICY_LEAST_WALKING];
		var r = document.getElementById("dvPolicy").getElementsByTagName("button");
		s = s || window.event;
		var t = s.srcElement || s.target,
			v;
		if (t.tagName.toLowerCase() == "button") {
			v = t.getAttribute("id").replace("policy", "");
			k.clearOverlays();
			o(w, u, x[v])
		}
	};

	function o(t, r, s) {
		var u = new BMap.TransitRoute(k, {
			renderOptions: {
				map: k
			},
			policy: s
		});
		u.setSearchCompleteCallback(function(x) {
			if (u.getStatus() == BMAP_STATUS_SUCCESS) {
				$("<div class='map_starticon'>起点:此房源所在位置</div>").appendTo($(".gongjiaoreasult"));
				for (var w = 0; w < x.getNumPlans(); w++) {
					var y = document.createElement("div");
					var v = d(k, x, w, y);
					y.style.lineHeight = "20px";
					y.onclick = v;
					y.className = "map_listitem";
					y.innerHTML = "<span style='color:#00a300;'>推荐线路" + (w + 1) + "</span><br/><span style='color:#f50;'>（" + x.getPlan(w).getDistance() + x.getPlan(w).getDuration() + "）</span><div style='padding-left:0.5em; padding-right:0.5em;'>" + x.getPlan(w).getDescription() + "</div>";
					document.getElementById("results").appendChild(y)
				}
				$("<div class='map_starticon'>终点:" + x.getEnd().title + "</div>").appendTo($(".gongjiaoreasult"))
			}
		});
		$("#results").html("");
		u.search(t, r)
	}
	$("#eat").bind("click", {
		word: "餐饮"
	}, p);
	$("#hotel").bind("click", {
		word: "酒店"
	}, p);
	$("#supermarket").bind("click", {
		word: "超市"
	}, p);
	$("#hospital").bind("click", {
		word: "医院"
	}, p);
	$("#movie").bind("click", {
		word: "影院"
	}, p);
	$("#ticket").bind("click", {
		word: "火车票"
	}, p);
	$("#bank").bind("click", {
		word: "银行"
	}, p);
	$("#scenic").bind("click", {
		word: "景点"
	}, p);
	$("#school").bind("click", {
		word: "学校"
	}, p);
	$("#fillsta").bind("click", {
		word: "加油站"
	}, p);
	$("#subway").bind("click", {
		word: "地铁站"
	}, p);

	function p(t) {
		k.clearOverlays();
		k.centerAndZoom(m, 16);
		var v = $("#this_houseposition").attr("lng");
		var u = $("#this_houseposition").attr("lat");
		var x = new BMap.Marker(m);
		k.addOverlay(x);
		x.setAnimation(BMAP_ANIMATION_BOUNCE);
		window.openInfoWinFuns = null;
		var y = {
			onSearchComplete: function(C) {
				if (w.getStatus() == BMAP_STATUS_SUCCESS) {
					var D = [];
					D.push('<div style="font-family: arial,sans-serif; border: 1px solid #eee; font-size: 12px;">');
					D.push('<div style="background: none repeat scroll 0% 0% rgb(255, 255, 255);">');
					D.push('<ol style="list-style: none outside none; padding: 0pt; margin: 0pt;">');
					openInfoWinFuns = [];
					for (var z = 0; z < C.getCurrentNumPois(); z++) {
						var A = s(C.getPoi(z).point, z);
						var B = r(A, C.getPoi(z), z);
						openInfoWinFuns.push(B);
						var E = "";
						if (z == 0) {
							E = "background-color:#f0f0f0;";
							B()
						}
						D.push('<li id="list' + z + '" style="margin: 2px 0pt; padding: 0pt 5px 0pt 3px; cursor: pointer; overflow: hidden; line-height: 17px;' + E + '" onclick="openInfoWinFuns[' + z + ']()">');
						D.push('<span style="float:left; width:6px; height:14px;background:url(/static/images/slist_1207/red_labels.png) 0 ' + (3 - z * 20) + 'px no-repeat;padding-left:10px;margin-right:3px"> </span>');
						D.push('<span style="color:#f60;text-decoration:underline">' + C.getPoi(z).title.replace(new RegExp(C.keyword, "g"), "<b>" + C.keyword + "</b>") + "</span>");
						D.push('<span style="color:#999999;"> - ' + C.getPoi(z).address + "</span>");
						D.push("</li>");
						D.push("")
					}
					D.push("</ol></div></div>")
				}
			}
		};

		function s(C, z) {
			var B = new BMap.Icon("http://api.map.baidu.com/img/markers.png", new BMap.Size(23, 25), {
				offset: new BMap.Size(10, 25),
				imageOffset: new BMap.Size(0, 0 - z * 25)
			});
			var A = new BMap.Marker(C, {
				icon: B
			});
			k.addOverlay(A);
			return A
		}

		function r(D, H, z) {
			var E = 10;
			var F = null;
			if (H.type == BMAP_POI_TYPE_NORMAL) {
				F = "地址：  "
			} else {
				if (H.type == BMAP_POI_TYPE_BUSSTOP) {
					F = "公交：  "
				} else {
					if (H.type == BMAP_POI_TYPE_SUBSTOP) {
						F = "地铁：  "
					}
				}
			}
			var C = '<div style="font-weight:bold;color:#CE5521;font-size:14px">' + H.title + "</div>";
			var B = [];
			B.push('<table cellspacing="0" style="table-layout:fixed;width:100%;font:12px arial,simsun,sans-serif"><tbody>');
			B.push("<tr>");
			B.push('<td style="vertical-align:top;line-height:16px;width:38px;white-space:nowrap;word-break:keep-all">' + F + "</td>");
			B.push('<td style="vertical-align:top;line-height:16px">' + H.address + " </td>");
			B.push("</tr>");
			if (typeof(H.phoneNumber) != "undefined") {
				B.push("<tr>");
				B.push('<td style="vertical-align:top;line-height:16px;width:38px;white-space:nowrap;word-break:keep-all">电话：</td>');
				B.push('<td style="vertical-align:top;line-height:16px">' + H.phoneNumber + " </td>");
				B.push("</tr>")
			}
			B.push("</tbody></table>");
			var A = new BMap.InfoWindow(B.join(""), {
				title: C,
				width: 200
			});
			var G = function() {
				D.openInfoWindow(A);
				for (var I = 0; I < E; I++) {
					if (!document.getElementById("list" + I)) {
						continue
					}
					if (I == z) {
						document.getElementById("list" + I).style.backgroundColor = "#f0f0f0"
					} else {
						document.getElementById("list" + I).style.backgroundColor = "#fff"
					}
				}
			};
			D.addEventListener("click", G);
			return G
		}
		var w = new BMap.LocalSearch(k, y);
		w.searchInBounds(t.data.word, k.getBounds())
	}
});