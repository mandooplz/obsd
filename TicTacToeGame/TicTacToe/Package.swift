// swift-tools-version: 6.2
// The swift-tools-version declares the minimum version of Swift required to build this package.

import PackageDescription

let package = Package(
    name: "TicTacToe",
    platforms: [.iOS(.v18), .macOS(.v15)],
    products: [
        // TicTacToe
        .library(
            name: "TicTacToe",
            targets: ["TicTacToe"]
        ),
    ],
    targets: [
        // TicTacToe
        .target(
            name: "TicTacToe"
        ),
        .testTarget(
            name: "TicTacToeTests",
            dependencies: ["TicTacToe"]
        ),
    ]
)
