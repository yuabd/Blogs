function miaovNext(d) {
	var b = null;
	var c = 0;
	var a = true;
	for (c = 0; c < d.length; c++) {
		b = d[c];
		b.speed = (b.target - b.cur) / 8;
		b.speed = b.speed > 0 ? Math.ceil(b.speed) : -Math.ceil(-b.speed);
		if (Math.abs(b.speed) > b.speedMax) {
			b.speed = (b.speed > 0) ? b.speedMax : -b.speedMax
		}
		b.cur += b.speed;
		if (b.cur != b.target) {
			a = false
		}
	}
	if (a) {
		for (c = 0; c < d.length; c++) {
			d[c].cur = d[c].target;
			d[c].speed = 0
		}
		return true
	}
	return false
}

function MiaovMoveLib(a, e, b, c) {
	var d = 0;
	this.motionDatas = [];
	for (d = 0; d < a.length; d++) {
		this.motionDatas[d] = {
			target: a[d],
			speed: 0,
			speedMax: e[d],
			cur: a[d]
		}
	}
	this.fnDoMove = b;
	this.fnMoveEnd = c;
	this.interval = 40;
	this.timer = null;
	this.enabled = true
}
MiaovMoveLib.prototype.setTarget = function (c) {
	var b = (new Date()).getTime();
	var d = true;
	var a = 0;
	for (a = 0; a < c.length; a++) {
		this.motionDatas[a].target = parseInt(c[a]);
		if (this.motionDatas[a].target != this.motionDatas[a].cur) {
			d = false
		}
	}
	if (d) {
		if (!this.timer) {
			this.start()
		}
		return
	}
	if (this.enabled) {
		if (!this.timer) {
			this.start()
		}
	}
};
MiaovMoveLib.prototype.setCurrent = function (b) {
	var a = 0;
	for (a = 0; a < b.length; a++) {
		this.motionDatas[a].cur = parseInt(b[a])
	}
};
MiaovMoveLib.prototype.start = function () {
	var a = this;
	if (!this.enabled) {
		return
	}
	if (this.timer) {
		clearInterval(this.timer)
	} else {
		this.timer = setInterval(function () {
			a.__timerHandler__()
		}, this.interval)
	}
	this.iStartTime = ((new Date()).getTime());
	this.iCounter = 0
};
MiaovMoveLib.prototype.stop = function () {
	if (this.timer) {
		clearInterval(this.timer);
		this.timer = null
	}
};
MiaovMoveLib.prototype.__timerHandler__ = function () {
	var a = false;
	a = miaovNext(this.motionDatas);
	if (a) {
		if (this.fnMoveEnd) {
			this.fnMoveEnd(this.motionDatas)
		}
		this.fnDoMove(this.motionDatas);
		this.stop()
	} else {
		this.iCounter++;
		this.fnDoMove(this.motionDatas)
	}
};

function MiaovQuirkyPopup(f, k, j, g, e, n, a, b, m, h, d) {
	var c = this;
	var i = a();
	var l = n();
	this.__oEleMove__ = f;
	this.__oEleDrag__ = k;
	this.__oEleBtn__ = j;
	this.__oMaxSize__ = e;
	this.__fnGetPos__ = n;
	this.__fnGetSize__ = a;
	this.__fnDoMove__ = b;
	this.__fnDoResize__ = m;
	this.__fnOnShowEnd__ = h;
	this.__fnOnHideEnd__ = d;
	this.__oDivOuter__ = document.createElement("div");
	this.__oDivOuter__.style.display = "none";
	this.__oDivOuter__.style.background = "white";
	this.__oDivOuter__.style.width = "100%";
	this.__oDivOuter__.style.filter = "alpha(opacity=0)";
	this.__oDivOuter__.style.opacity = "0";
	this.__oDivOuter__.style.top = "0px";
	this.__oDivOuter__.style.left = "0px";
	this.__oDivOuter__.style.position = "absolute";
	this.__oDivOuter__.style.zIndex = "3003";
	this.__oDivOuter__.style.overflow = "hidden";
	this.__oDivOuter__.style.height = document.body.offsetHeight + "px";
	document.body.appendChild(this.__oDivOuter__);
	this.__oDrag__ = new MiaovPerfectDrag(k, n, function (o, q) {
		var p = document.body.scrollTop || document.documentElement.scrollTop;
		if (o < 0) {
			o = 0
		} else {
			if (o + c.__oMaxSize__.x > document.body.offsetWidth) {
				o = document.body.offsetWidth - c.__oMaxSize__.x
			}
		} if (q < p) {
			q = p
		} else {
			if (q + c.__oMaxSize__.y > p + document.documentElement.clientHeight) {
				q = p + document.documentElement.clientHeight - c.__oMaxSize__.y
			}
		}
		f.style.left = o + "px";
		f.style.top = q + "px";
		c.__oSpeed__.x = o - c.__oLastPos__.x;
		c.__oSpeed__.y = q - c.__oLastPos__.y;
		c.__oLastPos__.x = o;
		c.__oLastPos__.y = q
	}, function () {
		c.__oLastPos__ = c.__fnGetPos__();
		c.stopMove();
		c.__oDivOuter__.style.display = "block"
	}, function () {
		c.startMove();
		c.__oDivOuter__.style.display = "none"
	});
	this.__oDrag__.disable();
	this.__oLastPos__ = {
		x: 0,
		y: 0
	};
	this.__oSpeed__ = {
		x: 0,
		y: 0
	};
	this.__oMoveTimer__ = null;
	this.__oMLResize__ = new MiaovMoveLib([i.x, i.y], [60, 60], function (o) {
		c.__fnDoMove__(l.x, l.y - o[1].cur / 2);
		c.__fnDoResize__(o[0].cur, o[1].cur)
	}, function () {
		c.__oDrag__.enable();
		c.startMove();
		g.onmousedown = function () {
			c.hide()
		}
	});
	this.__oMLMove__ = new MiaovMoveLib([0, 0], [40, 40], function (o) {
		c.__fnDoMove__(o[0].cur, o[1].cur)
	}, function () {
		c.startShowBtn();
		c.__oDock__.fnOnResizeOrScroll = function (o) {
			c.__oEleMove__.left = -c.__oMaxSize__.x + "px"
		}
	});
	this.__oMLBtn__ = new MiaovMoveLib([0], [40], function (o) {
		c.__oDock__.move({
			left: o[0].cur,
			top: 0
		})
	}, function () {
		if (this.isOpening) {
			c.__oSpeed__.x = 150 + Math.ceil(Math.random() * 150);
			c.__oSpeed__.y = 0;
			c.startMove();
			c.__oDrag__.enable();
			this.isOpening = false
		}
	});
	this.__oMLBtn__.isOpening = false;
	this.iAcc = 3;
	this.fScale = -0.7;
	this.__oEleBtn__.style.display = "block";
	this.__oDock__ = new Dock(j, DockType.LEFT | DockType.TOP, {
		left: -j.offsetWidth,
		top: 0
	}, null, null);
	this.__oEleBtn__.onclick = function () {
		var o = document.body.scrollTop || document.documentElement.scrollTop;
		f.style.top = o + "px";
		c.show()
	}
}
MiaovQuirkyPopup.prototype.initShow = function () {
	var a = this;
	this.__oMLResize__.setTarget([this.__oMaxSize__.x, this.__oMaxSize__.y])
};
MiaovQuirkyPopup.prototype.show = function () {
	this.__oDrag__.disable();
	this.stopMove();
	this.__oMLBtn__.setCurrent([0]);
	this.__oMLBtn__.setTarget([-this.__oEleBtn__.offsetWidth]);
	this.__oMLBtn__.isOpening = true
};
MiaovQuirkyPopup.prototype.hide = function () {
	var d = this;
	var a = this.__fnGetPos__();
	var b = this.__oDock__.getScreen();
	var c = document.body.scrollTop || document.documentElement.scrollTop;
	this.__oDrag__.disable();
	this.stopMove();
	this.__oMLMove__.setCurrent([a.x, a.y]);
	this.__oMLMove__.setTarget([-this.__oMaxSize__.x, b.top]);
	this.__oDock__.fnOnResizeOrScroll = function (e) {
		d.__oMLMove__.setTarget([-d.__oMaxSize__.x, e.top])
	}
};
MiaovQuirkyPopup.prototype.startShowBtn = function () {
	this.__oMLBtn__.setCurrent([-this.__oEleBtn__.offsetWidth]);
	this.__oMLBtn__.setTarget([0])
};
MiaovQuirkyPopup.prototype.startMove = function () {
	var a = this;
	if (this.__oMoveTimer__) {
		clearInterval(this.__oMoveTimer__)
	}
	this.__oMoveTimer__ = setInterval(function () {
		a.__doMove__()
	}, 30)
};
MiaovQuirkyPopup.prototype.stopMove = function () {
	clearInterval(this.__oMoveTimer__);
	this.__oMoveTimer__ = null
};
MiaovQuirkyPopup.prototype.__doMove__ = function () {
	var c = this.__fnGetPos__();
	var e = document.body.offsetWidth - this.__oMaxSize__.x;
	var d = document.body.scrollTop || document.documentElement.scrollTop;
	var a = d + document.documentElement.clientHeight - this.__oMaxSize__.y;
	this.__oSpeed__.y += this.iAcc;
	c.x += this.__oSpeed__.x;
	c.y += this.__oSpeed__.y;
	if (Math.abs(this.__oSpeed__.x) < 1) {
		this.__oSpeed__.x = 0
	}
	if (Math.abs(this.__oSpeed__.y) < 1) {
		this.__oSpeed__.y = 0
	}
	if (c.x <= 0) {
		c.x = 0;
		this.__oSpeed__.x *= this.fScale
	} else {
		if (c.x >= e) {
			c.x = e;
			this.__oSpeed__.x *= this.fScale
		}
	} if (c.y <= d) {
		c.y = d;
		this.__oSpeed__.y *= this.fScale
	} else {
		if (c.y >= a) {
			c.y = a;
			this.__oSpeed__.y *= this.fScale;
			this.__oSpeed__.x *= -this.fScale
		}
	} if (Math.abs(this.__oSpeed__.x) > 0 || Math.abs(this.__oSpeed__.y) > 0) {
		this.__fnDoMove__(c.x, c.y)
	}
};
if (typeof DockType == "undefined") {
	DockType = {
		LEFT: 1,
		RIGHT: 2,
		TOP: 4,
		BOTTOM: 8
	}
}

function Dock(b, e, a, c, d) {
	var f = false;
	var g = this;
	this.__oEle__ = b;
	this.__iDir__ = e;
	this.__oDis__ = a;
	this.fnOnResizeOrScroll = d;
	if (-1 != window.navigator.userAgent.indexOf("MSIE 6.0")) {
		if (-1 != window.navigator.userAgent.indexOf("MSIE 7.0") || -1 != window.navigator.userAgent.indexOf("MSIE 8.0")) {
			f = false
		} else {
			f = true
		}
	} else {
		f = false
	}
	this.bIsIe6 = f;
	if (c) {
		c(f)
	}
	if (f) {
		b.style.position = "absolute"
	} else {
		b.style.position = "fixed"
	} if (f) {
		miaovAppendEventListener(window, "scroll", function () {
			g.fixItem()
		})
	}
	miaovAppendEventListener(window, "resize", function () {
		g.fixItem()
	});
	this.fixItem()
}
Dock.prototype.getScreen = function () {
	var a = document.body.scrollTop || document.documentElement.scrollTop;
	return {
		left: 0,
		right: document.documentElement.clientWidth,
		top: a,
		bottom: a + document.documentElement.clientHeight
	}
};
Dock.prototype.move = function (a) {
	this.__oDis__ = a;
	this.fixItem()
};
Dock.prototype.fixItem = function () {
	var a = document.body.scrollTop || document.documentElement.scrollTop;
	if (this.__iDir__ & DockType.LEFT) {
		this.__oEle__.style.left = this.__oDis__.left + "px"
	} else {
		if (this.__iDir__ & DockType.RIGHT) {
			this.__oEle__.style.left = document.documentElement.clientWidth - this.__oDis__.right - this.__oEle__.offsetWidth + "px"
		} else {
			if (this.__iDir__ & DockType.BOTTOM) {
				if (this.bIsIe6) {
					this.__oEle__.style.top = a + document.documentElement.clientHeight - this.__oDis__.bottom - this.__oEle__.offsetHeight
				} else {
					this.__oEle__.style.top = document.documentElement.clientHeight - this.__oDis__.bottom - this.__oEle__.offsetHeight
				}
			} else {
				if (this.__iDir__ & DockType.TOP) {
					if (this.bIsIe6) {
						this.__oEle__.style.top = a + this.__oDis__.top + "px"
					} else {
						this.__oEle__.style.top = this.__oDis__.top + "px"
					}
				}
			}
		}
	} if (this.fnOnResizeOrScroll) {
		this.fnOnResizeOrScroll({
			left: 0,
			right: document.documentElement.clientWidth,
			top: a,
			bottom: a + document.documentElement.clientHeight
		})
	}
};

function MiaovPerfectDrag(a, d, b, f, c) {
	var e = this;
	this.oElement = a;
	this.oElement.style.overflow = "hidden";
	this.fnGetPos = d;
	this.fnDoMove = b;
	this.fnOnDragStart = f;
	this.fnOnDragEnd = c;
	this.__oStartOffset__ = {
		x: 0,
		y: 0
	};
	this.oElement.onmousedown = function (g) {
		e.startDrag(window.event || g)
	};
	this.fnOnMouseUp = function (g) {
		e.stopDrag(window.event || g)
	};
	this.fnOnMouseMove = function (g) {
		e.doDrag(window.event || g)
	}
}
MiaovPerfectDrag.prototype.enable = function () {
	var a = this;
	this.oElement.onmousedown = function (b) {
		a.startDrag(window.event || b)
	}
};
MiaovPerfectDrag.prototype.disable = function () {
	this.oElement.onmousedown = null
};
MiaovPerfectDrag.prototype.startDrag = function (b) {
	var c = this.fnGetPos();
	var a = b.clientX;
	var d = b.clientY;
	if (this.fnOnDragStart) {
		this.fnOnDragStart()
	}
	this.__oStartOffset__.x = a - c.x;
	this.__oStartOffset__.y = d - c.y;
	if (this.oElement.setCapture) {
		this.oElement.setCapture();
		this.oElement.onmouseup = this.fnOnMouseUp;
		this.oElement.onmousemove = this.fnOnMouseMove
	} else {
		document.addEventListener("mouseup", this.fnOnMouseUp, true);
		document.addEventListener("mousemove", this.fnOnMouseMove, true);
		window.captureEvents(Event.MOUSEMOVE | Event.MOUSEUP)
	}
};
MiaovPerfectDrag.prototype.stopDrag = function (a) {
	if (this.oElement.releaseCapture) {
		this.oElement.releaseCapture();
		this.oElement.onmouseup = null;
		this.oElement.onmousemove = null
	} else {
		document.removeEventListener("mouseup", this.fnOnMouseUp, true);
		document.removeEventListener("mousemove", this.fnOnMouseMove, true);
		window.releaseEvents(Event.MOUSE_MOVE | Event.MOUSE_UP)
	} if (this.fnOnDragEnd) {
		if (a.clientX == this.__oStartOffset__.x && a.clientY == this.__oStartOffset__.y) {
			this.fnOnDragEnd(false)
		} else {
			this.fnOnDragEnd(true)
		}
	}
};
MiaovPerfectDrag.prototype.doDrag = function (b) {
	var a = b.clientX;
	var c = b.clientY;
	this.fnDoMove(a - this.__oStartOffset__.x, c - this.__oStartOffset__.y)
};

function miaovAppendEventListener(c, b, a) {
	if (c.attachEvent) {
		c.attachEvent("on" + b, a)
	} else {
		c.addEventListener(b, a, true)
	}
}
window.onload = function () {
	var a = document.getElementById("messageBoardContainer");
	var b = a.getElementsByTagName("div")[0];
	var g = a.getElementsByTagName("div")[2];
	var j = g.getElementsByTagName("span");
	var f = a.getElementsByTagName("a")[0];
	var m = document.getElementById("quirkyPopupShowBtn");
	var k = 321;
	var e = 96;
	var c = 0;
	var l = document.body.scrollTop || document.documentElement.scrollTop;
	a.style.left = (document.documentElement.clientWidth - k) / 2 + "px";
	a.style.top = l + (document.documentElement.clientHeight) / 2 + "px";
	for (c = 0; c < j.length; c++) {
		j[c].onmousedown = function (i) {
			var h = window.event || i;
			if (h.stopPropagation) {
				h.stopPropagation()
			} else {
				h.cancelBubble = true
			}
		}
	}
	var d = new MiaovQuirkyPopup(a, a, m, f, {
		x: k,
		y: e
	}, function () {
		return {
			x: a.offsetLeft,
			y: a.offsetTop
		}
	}, function () {
		return {
			x: a.offsetWidth,
			y: a.offsetHeight
		}
	}, function (h, i) {
		a.style.left = h + "px";
		a.style.top = i + "px"
	}, function (h, i) {
		b.style.top = (i - e) / 2 + "px";
		b.style.left = (h - k) / 2 + "px";
		a.style.width = h + "px";
		a.style.height = i + "px"
	});
	setTimeout(function () {
		d.initShow()
	}, 500);
};