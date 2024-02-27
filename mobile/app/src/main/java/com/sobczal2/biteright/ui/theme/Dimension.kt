package com.sobczal2.biteright.ui.theme

import androidx.compose.foundation.shape.CornerBasedShape
import androidx.compose.foundation.shape.CornerSize
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.ShapeDefaults
import androidx.compose.material3.Shapes
import androidx.compose.ui.unit.dp

object Dimension {
    val xs = 8.dp
    val sm = 16.dp
    val md = 24.dp
    val lg = 32.dp
    val xl = 48.dp
    val xxl = 64.dp
    val xxxl = 96.dp
}

val MaterialTheme.dimension: Dimension
    get() = Dimension

val Shapes.extraSmallTop: CornerBasedShape
    get() = ShapeDefaults.ExtraSmall.copy(bottomStart = CornerSize(0.dp), bottomEnd = CornerSize(0.dp))

val Shapes.extraSmallBottom: CornerBasedShape
    get() = ShapeDefaults.ExtraSmall.copy(topStart = CornerSize(0.dp), topEnd = CornerSize(0.dp))

val Shapes.extraSmallStart: CornerBasedShape
    get() = ShapeDefaults.ExtraSmall.copy(topEnd = CornerSize(0.dp), bottomEnd = CornerSize(0.dp))

val Shapes.extraSmallEnd: CornerBasedShape
    get() = ShapeDefaults.ExtraSmall.copy(topStart = CornerSize(0.dp), bottomStart = CornerSize(0.dp))

val Shapes.smallTop: CornerBasedShape
    get() = ShapeDefaults.Small.copy(bottomStart = CornerSize(0.dp), bottomEnd = CornerSize(0.dp))

val Shapes.smallBottom: CornerBasedShape
    get() = ShapeDefaults.Small.copy(topStart = CornerSize(0.dp), topEnd = CornerSize(0.dp))

val Shapes.smallStart: CornerBasedShape
    get() = ShapeDefaults.Small.copy(topEnd = CornerSize(0.dp), bottomEnd = CornerSize(0.dp))

val Shapes.smallEnd: CornerBasedShape
    get() = ShapeDefaults.Small.copy(topStart = CornerSize(0.dp), bottomStart = CornerSize(0.dp))

val Shapes.mediumTop: CornerBasedShape
    get() = ShapeDefaults.Medium.copy(bottomStart = CornerSize(0.dp), bottomEnd = CornerSize(0.dp))

val Shapes.mediumBottom: CornerBasedShape
    get() = ShapeDefaults.Medium.copy(topStart = CornerSize(0.dp), topEnd = CornerSize(0.dp))

val Shapes.mediumStart: CornerBasedShape
    get() = ShapeDefaults.Medium.copy(topEnd = CornerSize(0.dp), bottomEnd = CornerSize(0.dp))

val Shapes.mediumEnd: CornerBasedShape
    get() = ShapeDefaults.Medium.copy(topStart = CornerSize(0.dp), bottomStart = CornerSize(0.dp))

val Shapes.largeTop: CornerBasedShape
    get() = ShapeDefaults.Large.copy(bottomStart = CornerSize(0.dp), bottomEnd = CornerSize(0.dp))

val Shapes.largeBottom: CornerBasedShape
    get() = ShapeDefaults.Large.copy(topStart = CornerSize(0.dp), topEnd = CornerSize(0.dp))

val Shapes.largeStart: CornerBasedShape
    get() = ShapeDefaults.Large.copy(topEnd = CornerSize(0.dp), bottomEnd = CornerSize(0.dp))

val Shapes.largeEnd: CornerBasedShape
    get() = ShapeDefaults.Large.copy(topStart = CornerSize(0.dp), bottomStart = CornerSize(0.dp))

val Shapes.extraLargeTop: CornerBasedShape
    get() = ShapeDefaults.ExtraLarge.copy(bottomStart = CornerSize(0.dp), bottomEnd = CornerSize(0.dp))

val Shapes.extraLargeBottom: CornerBasedShape
    get() = ShapeDefaults.ExtraLarge.copy(topStart = CornerSize(0.dp), topEnd = CornerSize(0.dp))

val Shapes.extraLargeStart: CornerBasedShape
    get() = ShapeDefaults.ExtraLarge.copy(topEnd = CornerSize(0.dp), bottomEnd = CornerSize(0.dp))

val Shapes.extraLargeEnd: CornerBasedShape
    get() = ShapeDefaults.ExtraLarge.copy(topStart = CornerSize(0.dp), bottomStart = CornerSize(0.dp))
